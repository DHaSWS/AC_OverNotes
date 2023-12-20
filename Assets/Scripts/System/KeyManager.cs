using OverNotes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour {
    // Singleton
    static private KeyManager _instance;
    static public KeyManager Instance { get { return _instance; } }

    // InputAction
    [Header("InputSystem")]
    [SerializeField] private InputActionReference[] laneActionReferences;
    [SerializeField] private string scheme = "Keyboard";

    private InputAction[] laneActions;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private bool[] _isFinishedRebind = new bool[4];

    // ------------------------------------------------------------------------
    [Space]
    // Key - Key guide
    [Header("KeyGuide - Text")]
    [SerializeField] private Text[] _keyGuideTexts = new Text[4];

    // Key - Key bind text
    [Header("KeyGuide - KeyBind")]
    [SerializeField] private Text[] _keyBindTexts = new Text[4];

    // ------------------------------------------------------------------------
    [Space]
    // Mask
    [Header("Mask")]
    [SerializeField] private GameObject mask;

    private void Awake() {
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        CheckValue();

        laneActions = new InputAction[laneActionReferences.Length];

        for (int i = 0; i < laneActionReferences.Length; i++) {
            laneActions[i] = laneActionReferences[i].action;
            _isFinishedRebind[i] = true;
        }

        RefreshKeyBind();
        RefreshKeyGuideText();

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy() {
        CleanUpOperation();
    }

    // Rebind -----------------------------------------------------------------
    public void StartRebinding() {
        CheckValue();

        var bindingIndex = laneActions[0].GetBindingIndex(InputBinding.MaskByGroup(scheme));

        // Nullification lane actions
        SetLaneActionsActive(false);

        for(int i = 0; i < _isFinishedRebind.Length; i++) {
            _isFinishedRebind[i] = false;
        }

        RefreshKeyBind();

        //レーンのバインドを行う
        OnStartRebinding(bindingIndex, 0);
    }

    // On start rebinding
    private void OnStartRebinding(int bindingIndex, int actionIndex) {
        rebindingOperation?.Cancel();

        // Display mask
        if(mask != null) {
            mask.SetActive(true);
        }

        // On finished rebind any key
        void OnFinishedRebindAnyKey(bool hideMask = true) {
            // Clean up operation
            CleanUpOperation();

            // Hidden mask
            if(mask != null && hideMask) {
                mask.SetActive(false);
            }
        }

        // Create rebind operation
        // and set callback
        // and start
        rebindingOperation = laneActions[actionIndex]
            .PerformInteractiveRebinding(bindingIndex)
            .OnComplete(_ => {
                if (SearchDuplicationkey(actionIndex)) {
                    OnFinishedRebindAnyKey(false);
                    OnStartRebinding(bindingIndex, actionIndex);
                } else {
                    // Load next bind
                    OnLoadNextBind(bindingIndex, actionIndex, OnFinishedRebindAnyKey);
                }
            })
            .OnCancel(_ => {
                OnFinishedRebindAnyKey();
            })
            .OnMatchWaitForAnother(Time.fixedDeltaTime)
            .Start();
    }

    // Search duplivation key : 重複キーがあるか検索する
    private bool SearchDuplicationkey(int nowIndex) {
        bool result = false;

        string thisBindKey = laneActions[nowIndex].GetBindingDisplayString();

        for(int i = nowIndex - 1; i >=0; i--) {
            string indexKey = laneActions[i].GetBindingDisplayString();

            // Is this bind key duplicated
            if(thisBindKey == indexKey) {
                result = true;
            }
        }

        return result;
    }

    // On load next bind
    private void OnLoadNextBind(int bindingIndex, int actionIndex, Action<bool> OnFinished) {
        // Binded key
        string bindKey = laneActions[actionIndex].GetBindingDisplayString();

        // Is bind key "Esc"
        if(bindKey == "Esc") {
            // Restart
            for(int i = 0; i < _isFinishedRebind.Length; i++) {
                _isFinishedRebind[i] = false;
            }

            OnFinished(false);
            RefreshKeyBind();
            OnStartRebinding(bindingIndex, 0);
            return;
        }

        _isFinishedRebind[actionIndex] = true;

        RefreshKeyBind();

        int nextActionIndex = actionIndex + 1;

        if(nextActionIndex <= laneActions.Length - 1) {
            OnFinished(false);
            OnStartRebinding(bindingIndex , nextActionIndex);
        } else {
            SetLaneActionsActive(true);
            OnFinished(true);
        }
    }

    // Set lane actions active
    private void SetLaneActionsActive(bool active) {
        foreach(var action in laneActions) {
            if (active) {
                action.Enable();
            } else {
                action.Disable();
            }
        }
    }

    // メモリリーク回避用
    private void CleanUpOperation() {
        rebindingOperation?.Dispose();
        rebindingOperation = null;
    }

    // ------------------------------------------------------------------------

    private void Update() {
        CheckValue();
        RefreshKeyGuideText();
    }

    private void RefreshKeyBind() {
        CheckValue();
        // バインドされたものを入れる
        for(int i = 0; i < _keyBindTexts.Length; i++) {
            if (_isFinishedRebind[i]) {
                _keyBindTexts[i].text = laneActions[i].GetBindingDisplayString();
            } else {
                _keyBindTexts[i].text = "";
            }
        }
    }

    private void RefreshKeyGuideText() {
        CheckValue();

        for(int i=0; i< _keyBindTexts.Length;i++) {
            if (_keyGuideTexts[i].text != GuideMessage.GuideLanes[i]){
                _keyGuideTexts[i].text = GuideMessage.GuideLanes[i];
                Debug.Log($"Changed key{i} guide text : {GuideMessage.GuideLanes[i]}");
            }
        }
    }

    private void CheckValue() {
        if (laneActionReferences == null ||
            laneActionReferences.Length != 4) {
            throw new System.Exception("Laneちゃんと設定しようね");
        }

        for(int i = 0; i < 4; i++) {
            if (_keyGuideTexts[i] == null) {
                throw new System.Exception($"Key{i} guide text is not set");
            }
            if (_keyBindTexts[i] == null) {
                throw new System.Exception($"Key{i} bind text is not set");
            }
        }
    }
}

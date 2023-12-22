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

    public bool[] IsFinishedRebind { get; private set; } = new bool[4];

    // ------------------------------------------------------------------------
    [Space]
    // Key - Key guide
    [Header("KeyGuide - Text")]
    [SerializeField] private Text[] _keyGuideTexts = new Text[4];

    // Key - Key bind text
    [Header("KeyGuide - KeyBind")]
    [SerializeField] public Text[] KeyBindTexts = new Text[4];

    // Key - GameObject
    [Header("KeuGuide - CanvasGroup")]
    [SerializeField] private CanvasGroup[] _keyGuideGruop = new CanvasGroup[4];

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
            IsFinishedRebind[i] = true;
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

        for(int i = 0; i < IsFinishedRebind.Length; i++) {
            IsFinishedRebind[i] = false;
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
            for(int i = 0; i < IsFinishedRebind.Length; i++) {
                IsFinishedRebind[i] = false;
            }

            OnFinished(false);
            RefreshKeyBind();
            OnStartRebinding(bindingIndex, 0);
            return;
        }

        IsFinishedRebind[actionIndex] = true;

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
        for(int i = 0; i < KeyBindTexts.Length; i++) {
            if (IsFinishedRebind[i]) {
                KeyBindTexts[i].text = laneActions[i].GetBindingDisplayString();
            } else {
                KeyBindTexts[i].text = "";
            }
        }
    }

    private void RefreshKeyGuideText() {
        CheckValue();

        for(int i=0; i< KeyBindTexts.Length;i++) {
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
            if (KeyBindTexts[i] == null) {
                throw new System.Exception($"Key{i} bind text is not set");
            }
        }
    }

    // Set visible
    public void SetVisible(int index, bool visible) {
        if (visible) {
            _keyGuideGruop[index].alpha = 1.0f;
        } else {
            _keyGuideGruop[index].alpha = 0.0f;
        }
    }

    // Set all objects visible
    public void SetVisible(bool visible) {
        for(int i = 0; i < 4; i++) {
            SetVisible(i, visible);
        }
    }

    public void SetVisible(bool lane1, bool lane2, bool lane3, bool lane4) {
        SetVisible(0, lane1);
        SetVisible(1, lane2);
        SetVisible(2, lane3);
        SetVisible(3, lane4);
    }
}

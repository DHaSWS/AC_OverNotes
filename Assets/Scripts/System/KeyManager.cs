using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour {
    // Singleton
    static private KeyManager instance;
    static public KeyManager Instance { get { return instance; } }

    // InputAction
    [Header("InputSystem")]
    [SerializeField] private InputActionReference[] laneActionReferences;
    [SerializeField] private string scheme = "Keyboard";
    private InputAction[] laneActions;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    // ------------------------------------------------------------------------
    [Space]
    // Key - Key guide
    [Header("KeyGuide - Text")]
    [SerializeField] private Text key1GuideText;
    [SerializeField] private Text key2GuideText;
    [SerializeField] private Text key3GuideText;
    [SerializeField] private Text key4GuideText;
    // Key - Key bind text
    [Header("KeyGuide - KeyBind")]
    [SerializeField] private Text key1Bind;
    [SerializeField] private Text key2Bind;
    [SerializeField] private Text key3Bind;
    [SerializeField] private Text key4Bind;
    // ------------------------------------------------------------------------
    [Space]
    // Mask
    [Header("Mask")]
    [SerializeField] private GameObject mask;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        CheckValue();

        laneActions = new InputAction[laneActionReferences.Length];

        for(int i = 0; i < laneActionReferences.Length; i++) {
            laneActions[i] = laneActionReferences[i].action;
        }

        RefreshKeyBind();
        RefreshKeyGuideText();
    }

    private void OnDestroy() {
        CleanUpOperation();
    }

    // Rebind -----------------------------------------------------------------
    public void StartRebinding() {
        CheckValue();
        if(mask != null) {
            mask.SetActive(true);
        }
        bool result = false;
        // すべてキーバインドできるまでループさせる
        while (!result) {
            foreach (InputAction action in laneActions) {
                var bindingIndex = action.GetBindingIndex(InputBinding.MaskByGroup(scheme));
                // レーンのバインドを行う
                result = OnStartRebinding(action, bindingIndex);
                // バインドに失敗したら
                if (!result) {
                    // ループを抜ける
                    break;
                }
            }
        }
        if (mask != null) {
            mask.SetActive(false);
        }
    }

    public bool OnStartRebinding(InputAction action, int bindingIndex) {
        rebindingOperation?.Cancel();
        action.Disable();

        bool result = true;

        void OnFinished(bool hideMask = true) {
            CleanUpOperation();

            action.Enable();
        }

        // ここでリバインドを行うらしい
        rebindingOperation = action
            .PerformInteractiveRebinding(bindingIndex)
            .OnComplete(_ => {
                OnFinished();
            })
            .OnCancel(_ => {
                // キャンセル処理
                OnFinished();
                result = false;
            })
            .OnMatchWaitForAnother(0.2f)
            .WithCancelingThrough("<Keyboard>/escape")
            .Start();   // リバインド開始

        return result;
    }

    // メモリリーク回避用
    private void CleanUpOperation() {
        rebindingOperation?.Dispose();
        rebindingOperation = null;
    }

    private void Update() {
        CheckValue();
        RefreshKeyGuideText();
    }

    private void RefreshKeyBind() {
        CheckValue();
        // バインドされたものを入れる
        key1Bind.text = laneActions[0].GetBindingDisplayString();
        key2Bind.text = laneActions[1].GetBindingDisplayString();
        key3Bind.text = laneActions[2].GetBindingDisplayString();
        key4Bind.text = laneActions[3].GetBindingDisplayString();
    }

    private void RefreshKeyGuideText() {
        CheckValue();
        if (key1GuideText.text != GuideMessage.guideLane1) {
            key1GuideText.text = GuideMessage.guideLane1;
        }
        if (key2GuideText.text != GuideMessage.guideLane2) {
            key2GuideText.text = GuideMessage.guideLane2;
        }
        if (key3GuideText.text != GuideMessage.guideLane3) {
            key3GuideText.text = GuideMessage.guideLane3;
        }
        if (key4GuideText.text != GuideMessage.guideLane4) {
            key4GuideText.text = GuideMessage.guideLane4;
        }
    }

    private void CheckValue() {
        if (laneActionReferences == null ||
            laneActionReferences.Length != 4) {
            throw new System.Exception("Laneちゃんと設定しようね");
        }
        if (key1GuideText == null ||
            key2GuideText == null ||
            key3GuideText == null ||
            key4GuideText == null) {
            throw new System.Exception("KeyGuideTextちゃんと設定しようね");
        }
        if (key1Bind == null ||
            key2Bind == null ||
            key3Bind == null ||
            key4Bind == null) {
            throw new System.Exception("KeyBindちゃんと設定しようね");
        }
    }
}

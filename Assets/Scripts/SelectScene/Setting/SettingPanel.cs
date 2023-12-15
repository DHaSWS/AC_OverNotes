using FancyScrollView;
using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class SettingPanel : FancyCell<SettingPanelItemData> {
    [SerializeField] private Text itemName = default;
    [SerializeField] private Text itemValue = default;
    [SerializeField] private Text itemGuide = default;
    [SerializeField] private Animator animator = default;
    [SerializeField] private int _index = default;
    float currentPosition = 0;

    static class AnimatorHash {
        public static readonly int Scroll = Animator.StringToHash("SettingCell");
        public static readonly int ValueColor = Animator.StringToHash("SettingCellColor");
    }

    private void Update() {
        OverNotesSystem system = OverNotesSystem.Instance;
        if(itemValue.text != system.SettingItems[_index].GetValue().ToString()) {
            itemValue.text = system.SettingItems[_index].GetValue().ToString();
        }

        if (
            SettingPanelParams.SettingIndex == _index &&
            SelectContext.selectRoutine == SelectContext.SelectRoutine.Setting_Value &&
            SettingPanelParams.IsTriggered
            ) {
            Debug.Log("Triggered");

            SettingPanelParams.IsTriggered = false;
        }

        //if (
        //    SelectContext.selectRoutine == SelectContext.SelectRoutine.Setting &&
        //    animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("ValueColor Layer")).normalizedTime <= 0.01f
        //    ) {
        //    animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("ValueColor Layer")).
        //}
    }

    public override void UpdateContent(SettingPanelItemData itemData) {
        itemName.text = itemData.Item.Name;
        itemGuide.text = itemData.Item.GuideName;
        itemValue.text = itemData.Item.GetValue().ToString();
        _index = itemData.Index;
        
    }

    public override void UpdatePosition(float position) {
        currentPosition = position;
        if (animator.isActiveAndEnabled) {
            animator.Play(AnimatorHash.Scroll, animator.GetLayerIndex("Base Layer"), position);
        }
        animator.speed = 0;
    }

    private void OnEnable() => UpdatePosition(currentPosition);
}

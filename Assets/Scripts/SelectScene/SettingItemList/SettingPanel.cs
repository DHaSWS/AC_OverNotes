using FancyScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class SettingPanel : FancyCell<SettingPanel_ItemData> {
    [SerializeField] private Text itemName = default;
    [SerializeField] private Text itemValue = default;
    [SerializeField] private Text itemGuide = default;
    [SerializeField] private Animator animator = default;
    float currentPosition = 0;

    static class AnimatorHash {
        public static readonly int Scroll = Animator.StringToHash("SettingCell");
    }

    public override void UpdateContent(SettingPanel_ItemData itemData) {
        itemName.text = itemData.ItemText;
        itemValue.text = itemData.ItemValue;
        itemGuide.text = itemData.ItemGuide;
    }

    public override void UpdatePosition(float position) {
        currentPosition = position;
        if (animator.isActiveAndEnabled) {
            animator.Play(AnimatorHash.Scroll, -1, position);
        }
        animator.speed = 0;
    }

    private void OnEnable() => UpdatePosition(currentPosition);
}

using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;
using OverNotes;

class ChartCell : FancyCell<Chart_ItemData>
{
    [SerializeField] Text diffucult = default;
    [SerializeField] Text level = default;
    [SerializeField] Animator animator = default;
    float currentPosition = 0;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    public override void UpdateContent(Chart_ItemData itemData)
    {
        diffucult.text = itemData.m_difficult;
        level.text = "Level : " + itemData.m_level;
    }

    public override void UpdatePosition(float position)
    {
        currentPosition = position;
        if (animator.isActiveAndEnabled)
        {
            animator.Play(AnimatorHash.Scroll, -1, position);
        }
        animator.speed = 0;
    }
    void OnEnable() => UpdatePosition(currentPosition);
}
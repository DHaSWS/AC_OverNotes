using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;
using OverNotes;
using TMPro;

class SongCell : FancyCell<ItemData>
{
    [SerializeField] Text message = default;
    [SerializeField] Animator animator = default;
    [SerializeField] TMP_Text textTitle;
    [SerializeField] TMP_Text textArtist;

    float currentPosition = 0;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    public override void UpdateContent(ItemData itemData)
    {
        message.text = itemData.m_title;
        textTitle.text = itemData.m_title;
        textArtist.text = itemData.m_artist;
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
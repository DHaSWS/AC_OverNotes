using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUpdator : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private int combo;
    [SerializeField] private Animator animator;

    private class AnimatorHash 
    {
        public static readonly int UpdateAnim = Animator.StringToHash("UpdateAnim");
    }

    private void Update()
    {
        if(combo != OverNotes.SystemData.PlayData.combo)
        {
            if(combo < OverNotes.SystemData.PlayData.combo)
            {
                animator.Play(AnimatorHash.UpdateAnim, 0, 0.0f);
            }
            combo = OverNotes.SystemData.PlayData.combo;
        }
    }
}

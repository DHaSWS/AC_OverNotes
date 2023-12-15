using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdator : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private float score = 0;

    [SerializeField] private Animator animator;

    private class AnimatorHash
    {
        public static readonly int UpdateAnim = Animator.StringToHash("UpdateAnim");
    }

    private void FixedUpdate()
    {
        if(score != ResultData.Score)
        {
            score = ResultData.Score;
            animator.Play(AnimatorHash.UpdateAnim, 0, 0.0f);
            scoreText.text = Mathf.RoundToInt(score).ToString();
        }
    }
}

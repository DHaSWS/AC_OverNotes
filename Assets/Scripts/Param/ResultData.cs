using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes
{
    public class ResultData 
    {
        static public int[] Count = new int[]
        {
            0,
            0,
            0,
            0,
            0,
            0
        };

        static public float score = 0.0f;

        static public int maxCombo = 0;

        static public void SetScore(PlayContext.Judge judge)
        {
            float value = (SystemData.PlayData.allPerfectScore / SystemData.GetChart().maxCombo) * SystemData.PlayData.ScoreRate[(int)judge];
            score += value;
        }
    }
}

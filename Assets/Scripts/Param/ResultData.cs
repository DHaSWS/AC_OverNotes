using OverNotes.System;

namespace OverNotes {
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

        static public float Score = 0.0f;

        static public int MaxCombo = 0;

        static public void SetScore(PlayContext.Judge judge)
        {
            OverNotesSystem system = OverNotesSystem.Instance;
            float value = SystemConstants.AllPerfectScore / system.GetChart().maxCombo * SystemConstants.ScoreRate[(int)judge];
            Score += value;
        }
    }
}

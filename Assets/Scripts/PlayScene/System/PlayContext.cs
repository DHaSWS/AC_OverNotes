using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes 
{
    public class PlayContext
    {
        public enum Routine
        {
            FadeOut,
            Ready,
            Play,
            Finish,
            FadeIn
        }
        public enum Judge
        {
            PerfectPlus,
            Perfect,
            Great,
            Good,
            Bad,
            Miss,
        }

        static public Routine routine = Routine.FadeOut;

        static public double playDspTime = 0.0d;

        static public double lastBeatTime = 0.0d;

        static public double displayTime = 0.0d;

        static public Judge GetJudge(float time)
        {
            Judge judge = Judge.Bad;

            if (time < SystemData.PlayData.JudgmentWidth[(int)Judge.PerfectPlus])
            {
                judge = Judge.PerfectPlus;
            }
            else if (time < SystemData.PlayData.JudgmentWidth[(int)Judge.Perfect])
            {
                judge = Judge.Perfect;
            }
            else if (time < SystemData.PlayData.JudgmentWidth[(int)Judge.Great])
            {
                judge = Judge.Great;
            }
            else if (time < SystemData.PlayData.JudgmentWidth[(int)Judge.Good])
            {
                judge = Judge.Good;
            }

            return judge;
        }
    }
}


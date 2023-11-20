using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes 
{
    public class ResultContext : MonoBehaviour
    {
        public enum State
        {
            Fade_Out,
            Intro,
            Wait,
            Fade_In
        }

        static public State state = State.Fade_Out;
    }
}

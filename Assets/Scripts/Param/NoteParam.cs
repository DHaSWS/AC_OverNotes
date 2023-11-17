using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes
{
    public class NoteParam : MonoBehaviour
    {
        public enum NOTE_STATE
        {
            NONE,
            NORMAL,
            HOLD,
        }

        public double beatTime = 0.0f;
        public double beatEndTime = 0.0f;
        public int column = 1;
        public NOTE_STATE noteState = NOTE_STATE.NONE;
    }
}

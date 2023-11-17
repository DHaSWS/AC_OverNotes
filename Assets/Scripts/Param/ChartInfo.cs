using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes
{
    public class ChartInfo
    {
        public ChartDifficult diffucult = ChartDifficult.EASY;
        public string level = null;
        public string creator = null;

        public int maxCombo = 0;

        public List<NoteParam> notes = new List<NoteParam>();
    }
}

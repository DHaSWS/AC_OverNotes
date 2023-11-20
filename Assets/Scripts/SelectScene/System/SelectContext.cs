using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes
{
    public class SelectContext
    {
        public enum SelectRoutine 
        {
            Intro,
            Song,
            Song_Select,
            Chart_Back,
            Chart,
            Chart_Select,
            Setting_Speed_Back,
            Setting_Speed,
            Setting_Speed_Select,
        }
        
        static public SelectRoutine selectRoutine = SelectRoutine.Song;
    }
}

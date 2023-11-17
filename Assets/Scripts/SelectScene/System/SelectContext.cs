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
            Chart,
            Chart_Select,
            Chart_Back,
            Setting = 100,
        }
        
        static public SelectRoutine selectRoutine = SelectRoutine.Song;
    }
}

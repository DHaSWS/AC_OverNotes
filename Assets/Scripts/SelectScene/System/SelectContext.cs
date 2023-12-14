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
            Setting,
            Setting_Value,
            FadeIn,
        }
        
        static public SelectRoutine selectRoutine = SelectRoutine.Song;
    }
}

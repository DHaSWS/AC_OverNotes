using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes
{
    public class BeatmapData
    {
        public string title = null;
        public string artist = null;

        public double offset = 0.0d;

        public string audioFilePath = null;
        public AudioClip clip = null;

        public List<BPMParam> bpms = new List<BPMParam>();

        public List<ChartInfo> charts = new List<ChartInfo>();
    }
}

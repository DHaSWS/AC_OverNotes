using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverNotes {
    public class SystemData : MonoBehaviour {
        // Path
        static readonly public string beatmapPath = Application.dataPath + "\\Beatmaps";

        // Song
        static public List<BeatmapData> beatmaps = new List<BeatmapData>();

        static public int songIndex = 0;
        static public int chartIndex = 0;

        // Audio
        static public double nowTime = 0;
        static public double maxTime = 0;

        // Setting
        static public float noteSpeed = 18.0f;
        static public float offset = 0.0f;

        static public BeatmapData GetBeatmap() {
            BeatmapData beatmapData = beatmaps[songIndex];
            return beatmapData;
        }

        static public ChartInfo GetChart() {
            ChartInfo chartInfo = GetBeatmap().charts[chartIndex];
            return chartInfo;
        }

        public class PlayData {
            static public float[] JudgmentWidth = new float[]
            {
                0.024f,
                0.064f,
                0.096f,
                0.128f,
                0.192f,
            };

            static public float[] ScoreRate = new float[]
            {
                1.01f,
                1.0f,
                0.7f,
                0.4f,
                0.0f,
                0.0f,
            };

            static public int combo = 0;

            static public Transform[] lanes;

            static public int allPerfectScore = 1000000;
        }
    }
}

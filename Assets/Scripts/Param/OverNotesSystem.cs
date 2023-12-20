using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using OverNotes;

namespace OverNotes.System {
    public class OverNotesSystem : MonoBehaviour {
        // シングルトン
        static private OverNotesSystem instance;
        static public OverNotesSystem Instance { get { return instance; } }

        public readonly string BeatmapPath = Application.dataPath + SystemConstants.BeatmapPath;
        public List<BeatmapData> Beatmaps = new List<BeatmapData>();

        public int SongIndex = SystemConstants.SongIndex;
        public int ChartIndex = SystemConstants.ChartIndex;
        public double NowTime = SystemConstants.NowTime;

        // Setting
        public List<SettingItem> SettingItems = new() {
            new SettingItemValue("NoteSpeed", "ノーツの速度を変えます", 15.0f, 0.5f, 30.0f, 1.0f ),
            new SettingItemValue("Offset", "曲と譜面のズレを調節します", 0.0f, 5.0f, 200.0f, -200.0f),
            new SettingItemValue("LaneCoverSize", "レーン上部を隠すカバーの縦幅を変えます", 0.0f, 1.0f, 80.0f, 0.0f),
            new SettingItemValue("BGMRate", "曲の大きさを変えます", 70.0f, 1.0f, 100.0f, 0.0f),
            new SettingItemValue("SERate", "効果音の大きさを変えます", 70.0f, 1.0f, 100.0f, 0.0f),
            new SettingItemValue("PlaySERate", "プレイ時の効果音の大きさを変えます(SERateとは別)", 70.0f, 1.0f, 100.0f, 0.0f),
            new SettingItemBind("KeyBind", "現在のキー設定を変えます")
        };

        public float NoteSpeed = SystemConstants.NoteSpeed;
        public float Offset = SystemConstants.Offset;

        private void Awake() {
            if (instance != null) return;
            // インスタンスを保持する
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // ビートマップデータを取得する
        public BeatmapData GetBeatmap() {
            BeatmapData beatmapData = Beatmaps[SongIndex];
            return beatmapData;
        }

        // 譜面を取得する
        public ChartInfo GetChart() {
            ChartInfo chartInfo = GetBeatmap().charts[ChartIndex];
            return chartInfo;
        }
    }
}
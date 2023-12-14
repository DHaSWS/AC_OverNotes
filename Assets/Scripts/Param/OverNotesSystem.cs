using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using OverNotes.System;
using OverNotes;

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
    public List<SettingItem> settingItems = new() {
        new SettingItemValue("Value0", "値テスト", 0, 1, 0, 10),
        new SettingItemValue("Value1", "値テスト", 0.0f, 0.5f, 0.0f, 10.0f),
        new SettingItemToggle("Toggle0", "トグルテスト", false),
        new SettingItemToggle("Toggle1", "トグルテスト", true),
    };

    public float NoteSpeed = SystemConstants.NoteSpeed;
    public float Offset = SystemConstants.Offset;

    private void Awake() {
        if (instance != null) return;
        // インスタンスを保持する
        instance = this;
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
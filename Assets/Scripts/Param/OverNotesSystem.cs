using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using OverNotes.System;
using OverNotes;

public class OverNotesSystem : MonoBehaviour {
    // �V���O���g��
    static private OverNotesSystem instance;
    static public OverNotesSystem Instance { get { return instance; } }

    public readonly string BeatmapPath = Application.dataPath + SystemConstants.BeatmapPath;
    public List<BeatmapData> Beatmaps = new List<BeatmapData>();

    public int SongIndex = SystemConstants.SongIndex;
    public int ChartIndex = SystemConstants.ChartIndex;
    public double NowTime = SystemConstants.NowTime;

    // Setting
    public List<SettingItem> settingItems = new() {
        new SettingItemValue("Value0", "�l�e�X�g", 0, 1, 0, 10),
        new SettingItemValue("Value1", "�l�e�X�g", 0.0f, 0.5f, 0.0f, 10.0f),
        new SettingItemToggle("Toggle0", "�g�O���e�X�g", false),
        new SettingItemToggle("Toggle1", "�g�O���e�X�g", true),
    };

    public float NoteSpeed = SystemConstants.NoteSpeed;
    public float Offset = SystemConstants.Offset;

    private void Awake() {
        if (instance != null) return;
        // �C���X�^���X��ێ�����
        instance = this;
    }

    // �r�[�g�}�b�v�f�[�^���擾����
    public BeatmapData GetBeatmap() {
        BeatmapData beatmapData = Beatmaps[SongIndex];
        return beatmapData;
    }

    // ���ʂ��擾����
    public ChartInfo GetChart() {
        ChartInfo chartInfo = GetBeatmap().charts[ChartIndex];
        return chartInfo;
    }
}
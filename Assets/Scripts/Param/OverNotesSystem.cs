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
    public Dictionary<string, object> SettingData = new() {
        { "NoteSpeed", SystemConstants.NoteSpeed },
        { "Offset", SystemConstants.Offset },
        { "LaneCoverSize", SystemConstants.LaneCoverSize },
        { "BGMRate", SystemConstants.BGMRate },
        { "SERate", SystemConstants.SERate },
        { "PlaySERate", SystemConstants.PlaySERate },
        { "KeyBind", null }
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using OverNotes.System;

public class OverNotesSystem : MonoBehaviour {
    // シングルトン
    static private OverNotesSystem instance;
    static public OverNotesSystem Instance { get { return instance; } }

    public readonly string BeatmapPath = Application.dataPath + SystemConstants.BeatmapPath;

    public int SongIndex = SystemConstants.SongIndex;
    public int ChartIndex = SystemConstants.ChartIndex;
    public double NowTime = SystemConstants.NowTime;

    // Setting
    public float NoteSpeed = SystemConstants.NoteSpeed;
    public float Offset = SystemConstants.Offset;

    private void Awake() {
        if (instance != null) return;
        // インスタンスを保持する
        instance = this;
    }
}
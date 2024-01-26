using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OverNotes.System {
    public class SystemConstants {
        static public readonly string BeatmapPath = "\\Beatmaps";
        static public readonly float[] JudgementRange = {
            0.024f,
            0.064f,
            0.096f,
            0.128f,
            0.192f,
        };
        static public readonly float[] ScoreRate = {
            1.01f,
            1.0f,
            0.7f,
            0.4f,
            0.0f,
            0.0f,
        };

        static public readonly int AllPerfectScore = 1000000;

        static public readonly int SongIndex = 0;
        static public readonly int ChartIndex = 0;
        static public readonly double NowTime = 0;

        // Setting
        static public readonly float NoteSpeed = 15.0f;       // ノーツのスピード
        static public readonly float Offset = 0.0f;           // オフセット
        static public readonly float LaneCoverSize = 0.0f;    // レーンカバーのサイズ
        static public readonly float BGMRate = 0.7f;          // 曲のボリューム
        static public readonly float PlaySERate = 0.7f;       // プレイ時の効果音のボリューム

        // Setting item
        static public readonly List<List<object>> SettingItems = new() {
            new() { "NoteSpeed", "ノーツの速度を変えます", 15.0f, 15.0f, 0.5f, 30.0f, 1.0f },
            new() { "Offset", "曲と譜面のズレを調節します", 0.0f, 0.0f, 5.0f, 200.0f, -200.0f},
            new() { "LaneCoverSize", "レーン上部を隠すカバーの縦幅を変えます", 0.0f, 0.0f, 1.0f, 80.0f, 0.0f },
            new() { "BGMRate", "曲の大きさを変えます", 70.0f, 70.0f, 1.0f, 100.0f, 0.0f },
            new() { "PlaySERate", "プレイ時の効果音の大きさを変えます(SERateとは別)", 70.0f, 70.0f, 1.0f, 100.0f, 0.0f },
            new() { "KeyBind", "現在のキー設定を変えます"}
        };

        public enum SettingItemTag {
            NoteSpeed,
            Offset,
            LaneCoverSize,
            BGMRate,
            PlaySERate,
            KeyBind
        }
    }
}

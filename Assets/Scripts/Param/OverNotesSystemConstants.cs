using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OverNotes.System {
    public class SystemConstants {
        public const string BeatmapPath = "\\Beatmaps";
        public readonly float[] JudgementRange = {
            0.024f,
            0.064f,
            0.096f,
            0.128f,
            0.192f,
        };
        public readonly float[] ScoreRate = {
            1.01f,
            1.0f,
            0.7f,
            0.4f,
            0.0f,
            0.0f,
        };
        public const int SongIndex = 0;
        public const int ChartIndex = 0;
        public const double NowTime = 0;

        // Setting
        public const float NoteSpeed = 15.0f;       // ノーツのスピード
        public const float Offset = 0.0f;           // オフセット
        public const float LaneCoverSize = 0.0f;    // レーンカバーのサイズ
        public const float BGMRate = 0.7f;          // 曲のボリューム
        public const float SERate = 0.7f;           // 効果音のボリューム
        public const float PlaySERate = 0.7f;       // プレイ時の効果音のボリューム

        // GuideMessage
        //public Dictionary<string, string> SettingItemGuideMessage = new() {
        //    { "NoteSpeed", "ノーツの速度を変えます"},
        //    { "Offset", "曲と譜面のズレを調節します"},
        //    { "LaneCoverSize", "レーン上部を隠すカバーの縦幅を変えます"},
        //    { "BGMRate", "曲の大きさを変えます"},
        //    { "SERate", "効果音の大きさを変えます"},
        //    { "PlaySERate", "プレイ時の効果音の大きさを変えます(SERateとは別)"},
        //    { "KeyBind", "現在のキー設定を変えます"}
        //};

        // GuideName
        public enum GuideName {
            NoteSpeed,
            Offset,
            LaneCoverSize
        };

        public readonly List<string> SettingItemGuideMessage = new() {
            "ノーツの速度を変えます",
            "曲と譜面のズレを調節します",
            "レーン上部を隠すカバーの縦幅を変えます",
            "曲の大きさを変えます",
            "効果音の大きさを変えます",
            "プレイ時の効果音の大きさを変えます(SERateとは別)",
            "現在のキー設定を変えます",
        };
    }
}

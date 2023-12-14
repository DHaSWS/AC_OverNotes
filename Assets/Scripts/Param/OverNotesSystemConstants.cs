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
        public const float NoteSpeed = 15.0f;       // �m�[�c�̃X�s�[�h
        public const float Offset = 0.0f;           // �I�t�Z�b�g
        public const float LaneCoverSize = 0.0f;    // ���[���J�o�[�̃T�C�Y
        public const float BGMRate = 0.7f;          // �Ȃ̃{�����[��
        public const float SERate = 0.7f;           // ���ʉ��̃{�����[��
        public const float PlaySERate = 0.7f;       // �v���C���̌��ʉ��̃{�����[��

        // GuideMessage
        //public Dictionary<string, string> SettingItemGuideMessage = new() {
        //    { "NoteSpeed", "�m�[�c�̑��x��ς��܂�"},
        //    { "Offset", "�Ȃƕ��ʂ̃Y���𒲐߂��܂�"},
        //    { "LaneCoverSize", "���[���㕔���B���J�o�[�̏c����ς��܂�"},
        //    { "BGMRate", "�Ȃ̑傫����ς��܂�"},
        //    { "SERate", "���ʉ��̑傫����ς��܂�"},
        //    { "PlaySERate", "�v���C���̌��ʉ��̑傫����ς��܂�(SERate�Ƃ͕�)"},
        //    { "KeyBind", "���݂̃L�[�ݒ��ς��܂�"}
        //};

        // GuideName
        public enum GuideName {
            NoteSpeed,
            Offset,
            LaneCoverSize
        };

        public readonly List<string> SettingItemGuideMessage = new() {
            "�m�[�c�̑��x��ς��܂�",
            "�Ȃƕ��ʂ̃Y���𒲐߂��܂�",
            "���[���㕔���B���J�o�[�̏c����ς��܂�",
            "�Ȃ̑傫����ς��܂�",
            "���ʉ��̑傫����ς��܂�",
            "�v���C���̌��ʉ��̑傫����ς��܂�(SERate�Ƃ͕�)",
            "���݂̃L�[�ݒ��ς��܂�",
        };
    }
}

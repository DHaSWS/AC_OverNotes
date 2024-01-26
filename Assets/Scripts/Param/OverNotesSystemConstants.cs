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
        static public readonly float NoteSpeed = 15.0f;       // �m�[�c�̃X�s�[�h
        static public readonly float Offset = 0.0f;           // �I�t�Z�b�g
        static public readonly float LaneCoverSize = 0.0f;    // ���[���J�o�[�̃T�C�Y
        static public readonly float BGMRate = 0.7f;          // �Ȃ̃{�����[��
        static public readonly float PlaySERate = 0.7f;       // �v���C���̌��ʉ��̃{�����[��

        // Setting item
        static public readonly List<List<object>> SettingItems = new() {
            new() { "NoteSpeed", "�m�[�c�̑��x��ς��܂�", 15.0f, 15.0f, 0.5f, 30.0f, 1.0f },
            new() { "Offset", "�Ȃƕ��ʂ̃Y���𒲐߂��܂�", 0.0f, 0.0f, 5.0f, 200.0f, -200.0f},
            new() { "LaneCoverSize", "���[���㕔���B���J�o�[�̏c����ς��܂�", 0.0f, 0.0f, 1.0f, 80.0f, 0.0f },
            new() { "BGMRate", "�Ȃ̑傫����ς��܂�", 70.0f, 70.0f, 1.0f, 100.0f, 0.0f },
            new() { "PlaySERate", "�v���C���̌��ʉ��̑傫����ς��܂�(SERate�Ƃ͕�)", 70.0f, 70.0f, 1.0f, 100.0f, 0.0f },
            new() { "KeyBind", "���݂̃L�[�ݒ��ς��܂�"}
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

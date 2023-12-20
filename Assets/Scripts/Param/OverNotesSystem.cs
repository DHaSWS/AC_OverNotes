using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using OverNotes;

namespace OverNotes.System {
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
        public List<SettingItem> SettingItems = new() {
            new SettingItemValue("NoteSpeed", "�m�[�c�̑��x��ς��܂�", 15.0f, 0.5f, 30.0f, 1.0f ),
            new SettingItemValue("Offset", "�Ȃƕ��ʂ̃Y���𒲐߂��܂�", 0.0f, 5.0f, 200.0f, -200.0f),
            new SettingItemValue("LaneCoverSize", "���[���㕔���B���J�o�[�̏c����ς��܂�", 0.0f, 1.0f, 80.0f, 0.0f),
            new SettingItemValue("BGMRate", "�Ȃ̑傫����ς��܂�", 70.0f, 1.0f, 100.0f, 0.0f),
            new SettingItemValue("SERate", "���ʉ��̑傫����ς��܂�", 70.0f, 1.0f, 100.0f, 0.0f),
            new SettingItemValue("PlaySERate", "�v���C���̌��ʉ��̑傫����ς��܂�(SERate�Ƃ͕�)", 70.0f, 1.0f, 100.0f, 0.0f),
            new SettingItemBind("KeyBind", "���݂̃L�[�ݒ��ς��܂�")
        };

        public float NoteSpeed = SystemConstants.NoteSpeed;
        public float Offset = SystemConstants.Offset;

        private void Awake() {
            if (instance != null) return;
            // �C���X�^���X��ێ�����
            instance = this;
            DontDestroyOnLoad(gameObject);
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
}
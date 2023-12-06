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
        // -- MenuBGM
        public const float BPMRate = 0.7f;          // �Ȃ̃{�����[��
        public const float SERate = 0.7f;           // ���ʉ��̃{�����[��
        // -- PlayBGM
        public const float PlayBPMRate = 0.7f;      // �Ȃ̃{�����[��
        public const float PlaySERate = 0.7f;       // ���ʉ��̃{�����[��
    }
}

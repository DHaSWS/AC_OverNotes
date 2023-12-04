using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFactory : MonoBehaviour
{
    public GameObject m_notePrefab;

    public void CreateNote(
        double beatTime,
        double beatEndTime,
        int column
        )
    {
        // �m�[�c�̐���
        GameObject note = Instantiate(m_notePrefab, OverNotes.SystemData.PlayData.lanes[column - 1]);

        // �m�[�c�̃f�[�^
        NoteParam noteParam = note.GetComponent<NoteParam>();

        // �f�[�^���ɔ��莞�ԁA����I�����Ԃ�����
        noteParam.beatTime = beatTime;
        noteParam.beatEndTime = beatEndTime;

        noteParam.noteState = NoteParam.NOTE_STATE.NORMAL;

        // �������e�͈͓���������
        if(Mathf.Abs((float)(beatTime - beatEndTime)) >= 0.001f)
        {
            // �^�O��Long�ɂ���
            note.tag = "Long";
        }

        // ���[�J�����W��0�ɂ���
        note.transform.localPosition = Vector3.zero;

        // ��]�p��0�ɂ���
        note.transform.localRotation = Quaternion.identity;
    }
}

using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFactory : MonoBehaviour
{
    public GameObject m_notePrefab;

    public GameObject CreateNote(
        double beatTime,
        double beatEndTime,
        int column
        )
    {
        // ノーツの生成
        GameObject note = Instantiate(m_notePrefab, PlayData.Lanes[column - 1]);

        NoteController noteController = note.GetComponent<NoteController>();

        // ノーツのデータ
        NoteParam noteParam = noteController.param;

        // データ内に判定時間、判定終了時間を入れる
        noteParam.beatTime = beatTime;
        noteParam.beatEndTime = beatEndTime;

        noteParam.noteState = NoteParam.NOTE_STATE.NORMAL;

        // もし許容範囲内だったら
        if(Mathf.Abs((float)(beatTime - beatEndTime)) >= 0.001f)
        {
            // タグをLongにする
            note.tag = "Long";
        }

        // 回転角を0にする
        note.transform.localRotation = Quaternion.identity;

        return note;
    }
}

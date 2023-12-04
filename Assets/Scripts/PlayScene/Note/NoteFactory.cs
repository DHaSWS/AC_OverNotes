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
        // ノーツの生成
        GameObject note = Instantiate(m_notePrefab, OverNotes.SystemData.PlayData.lanes[column - 1]);

        // ノーツのデータ
        NoteParam noteParam = note.GetComponent<NoteParam>();

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

        // ローカル座標を0にする
        note.transform.localPosition = Vector3.zero;

        // 回転角を0にする
        note.transform.localRotation = Quaternion.identity;
    }
}

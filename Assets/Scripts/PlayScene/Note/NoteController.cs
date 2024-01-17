using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using static OverNotes.PlayContext;

public class NoteController : MonoBehaviour {
    [SerializeField] public NoteParam param;

    private void OnEnable() {
        param = new NoteParam();
    }

    public void OnUpdate() {
        // Update position
        OnUpdatePosition();
    }

    // On update position
    public void OnUpdatePosition() {
        switch (param.noteState) {
            case NoteParam.NOTE_STATE.NORMAL: {
                    UpdateNormal();
                    break;
                }
            case NoteParam.NOTE_STATE.HOLD: {
                    UpdateHold();
                    break;
                }
        }
    }

    private void UpdateNormal() {
        OverNotesSystem system = OverNotesSystem.Instance;
        float noteSpeed = (float)system.SettingItems[(int)SystemConstants.SettingItemTag.NoteSpeed].GetValue();

        Vector3 position = Vector3.zero;
        position.y = (float)(param.beatTime - system.NowTime) *
            noteSpeed;
        transform.localPosition = position;

        float length = 0.0f;
        length = (float)(param.beatEndTime - param.beatTime) * noteSpeed;
        length = Mathf.Max(0.25f, length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(1, length);
    }

    private void UpdateHold() {
        Vector3 position = Vector3.zero;
        transform.localPosition = position;

        OverNotesSystem system = OverNotesSystem.Instance;
        double nowTime = system.NowTime;
        float noteSpeed = (float)system.SettingItems[(int)SystemConstants.SettingItemTag.NoteSpeed].GetValue();

        float length = 0.0f;
        length = (float)(param.beatEndTime - nowTime) * noteSpeed;
        length = Mathf.Max(0.25f, length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(1, length);

        if (nowTime > param.beatEndTime) {
            JudgeHold(0.0f);
        }
    }

    /// <summary>
    /// Get sub time
    /// </summary>
    /// <returns>Sub time</returns>
    public float GetSubTime() {
        return (float)(OverNotesSystem.Instance.NowTime - param.beatEndTime);
    }

    /// <summary>
    /// Set judge miss
    /// </summary>
    public void SetJudgeMiss() {
        PlayData.Combo = 0;
        ResultData.Count[(int)PlayContext.Judge.Miss]++;
        ResultData.SetScore(PlayContext.Judge.Miss);

        GameObject judgeObj = GameObject.Find("UI/Judge");
        judgeObj.GetComponent<Judge>().SetAnimation(PlayContext.Judge.Miss);

        Destroy(gameObject);
    }

    public void JudgeNormal(float subTime) {
        if (subTime > SystemConstants.JudgementRange[(int)PlayContext.Judge.Bad]) {
            return;
        }
        PlayContext.Judge judge = GetJudge(subTime);

        GameObject judgeObj = GameObject.Find("UI/Judge");
        judgeObj.GetComponent<Judge>().SetAnimation(judge);

        ResultData.Count[(int)judge]++;

        if (judge < PlayContext.Judge.Bad) {
            PlayData.Combo++;
        } else {
            PlayData.Combo = 0;
        }

        ResultData.SetScore(judge);

        if (tag == "Long") {
            param.noteState = NoteParam.NOTE_STATE.HOLD;
        } else {
            Destroy(gameObject);
        }
    }

    public void JudgeHold(float subTime) {
        if (param.noteState != NoteParam.NOTE_STATE.HOLD) {
            return;
        }

        PlayContext.Judge judge = PlayContext.GetJudge(subTime);
        ResultData.Count[(int)judge]++;

        GameObject judgeObj = GameObject.Find("UI/Judge");
        judgeObj.GetComponent<Judge>().SetAnimation(judge);

        if (judge < PlayContext.Judge.Bad) {
            PlayData.Combo++;
        } else {
            PlayData.Combo = 0;
        }

        ResultData.SetScore(judge);

        Destroy(gameObject);
    }
}

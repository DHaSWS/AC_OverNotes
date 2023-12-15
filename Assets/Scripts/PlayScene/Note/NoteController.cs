using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteController : MonoBehaviour
{
    [SerializeField] public NoteParam param;

    private void OnEnable()
    {
        param = new NoteParam();
    }

    private void FixedUpdate()
    {
        OverNotesSystem system = OverNotesSystem.Instance;

        switch (param.noteState)
        {
            case NoteParam.NOTE_STATE.NORMAL:
                {
                    UpdateNormal();
                    break;
                }
            case NoteParam.NOTE_STATE.HOLD:
                {
                    UpdateHold();
                    break;
                }
        }

        double sub = (float)(system.NowTime - param.beatEndTime);

        if(sub > SystemConstants.JudgementRange[(int)PlayContext.Judge.Bad])
        {
            Destroy(gameObject);
            PlayData.Combo = 0;
            ResultData.Count[(int)PlayContext.Judge.Miss]++;
            ResultData.SetScore(PlayContext.Judge.Miss);
        }
    }

    private void UpdateNormal()
    {
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

    private void UpdateHold()
    {
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

        if(nowTime > param.beatEndTime)
        {
            JudgeHold(0.0f);
        }
    }

    public void JudgeNormal(float subTime)
    {
        if (subTime > SystemConstants.JudgementRange[(int)PlayContext.Judge.Bad])
        {
            return;
        }
        PlayContext.Judge judge = PlayContext.GetJudge(subTime);
        ResultData.Count[(int)judge]++;

        if (judge < PlayContext.Judge.Bad)
        {
            PlayData.Combo++;
        }
        else
        {
            PlayData.Combo = 0;
        }

        ResultData.SetScore(judge);

        if (tag == "Long")
        {
            param.noteState = NoteParam.NOTE_STATE.HOLD;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void JudgeHold(float subTime)
    {
        if(param.noteState != NoteParam.NOTE_STATE.HOLD)
        {
            return;
        }

        PlayContext.Judge judge = PlayContext.GetJudge(subTime);
        ResultData.Count[(int)judge]++;

        if (judge < PlayContext.Judge.Bad)
        {
            PlayData.Combo++;
        }
        else
        {
            PlayData.Combo = 0;
        }

        ResultData.SetScore(judge);

        Destroy(gameObject);
    }
}

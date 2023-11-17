using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteController : MonoBehaviour
{
    [SerializeField] public NoteParam param;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
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

        double sub = (float)(SystemData.nowTime - param.beatEndTime);

        if(sub > SystemData.PlayData.JudgmentWidth[(int)PlayContext.Judge.Bad])
        {
            Destroy(gameObject);
            SystemData.PlayData.combo = 0;
            ResultData.Count[(int)PlayContext.Judge.Miss]++;
            ResultData.SetScore(PlayContext.Judge.Miss);
        }
    }

    private void UpdateNormal()
    {
        Vector3 position = Vector3.zero;
        position.y = (float)(param.beatTime - SystemData.nowTime) *
            SystemData.noteSpeed;
        transform.localPosition = position;

        float length = 0.0f;
        length = (float)(param.beatEndTime - param.beatTime) * SystemData.noteSpeed;
        length = Mathf.Max(0.25f, length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(1, length);
    }

    private void UpdateHold()
    {
        Vector3 position = Vector3.zero;
        transform.localPosition = position;

        float length = 0.0f;
        length = (float)(param.beatEndTime - SystemData.nowTime) * SystemData.noteSpeed;
        length = Mathf.Max(0.25f, length);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(1, length);

        if(SystemData.nowTime > param.beatEndTime)
        {
            JudgeHold(0.0f);
        }
    }

    public void JudgeNormal(float subTime)
    {
        if (subTime > SystemData.PlayData.JudgmentWidth[(int)PlayContext.Judge.Bad])
        {
            return;
        }
        PlayContext.Judge judge = PlayContext.GetJudge(subTime);
        ResultData.Count[(int)judge]++;

        if (judge < PlayContext.Judge.Bad)
        {
            SystemData.PlayData.combo++;
        }
        else
        {
            SystemData.PlayData.combo = 0;
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
            SystemData.PlayData.combo++;
        }
        else
        {
            SystemData.PlayData.combo = 0;
        }

        ResultData.SetScore(judge);

        Destroy(gameObject);
    }
}

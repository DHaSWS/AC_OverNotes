using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OverNotes;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using OverNotes.System;

public class PlaySceneDirector : MonoBehaviour
{
    [SerializeField] NoteFactory noteFactory;
    [SerializeField] Image fadeImage;
    [SerializeField] Transform[] lanes;
    [Space]
    [Header("仮データ")]
    [SerializeField] BeatmapLoader beatmapLoader;
    List<NoteParam> notes;

    private void Awake()
    {
        PlayContext.Routine = PlayContext.PlayRoutine.FadeOut;
        PlayContext.PlayDspTime = 0.0d;
        PlayContext.LastBeatTime = 0.0d;
        PlayContext.DisplayTime = 0.0d;

        OverNotesSystem system = OverNotesSystem.Instance;

        system.NowTime = 0;
        

        ResultData.Count = new int[]
        {
            0,
            0,
            0,
            0,
            0,
            0
        };
        ResultData.Score = 0.0f;
        ResultData.MaxCombo = 0;

        GuideMessage.GuideLane1 = "";
        GuideMessage.GuideLane2 = "";
        GuideMessage.GuideLane3 = "";
        GuideMessage.GuideLane4 = "";

        PlayData.Lanes = lanes;

        notes = system.GetChart().notes;

        LoadChart();

        ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { PlayContext.Routine = PlayContext.PlayRoutine.Ready; });
    }

    private void FixedUpdate()
    {
        foreach (NoteParam note in notes)
        {
            if (note.beatTime <= PlayContext.DisplayTime && note.noteState == NoteParam.NOTE_STATE.NONE)
            {
                noteFactory.CreateNote(note.beatTime, note.beatEndTime, note.column);
                note.noteState = NoteParam.NOTE_STATE.NORMAL;
            }
        }

        if (PlayContext.Routine == PlayContext.PlayRoutine.FadeIn &&
            ONFade.Same(ONFade.State.Idle_FadeIn))
        {
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () =>
            {
                SceneManager.LoadScene("Scenes/ResultScene");
            });
        }

        if (OverNotesSystem.Instance.NowTime > PlayContext.LastBeatTime)
        {
            PlayContext.Routine = PlayContext.PlayRoutine.Finish;
        }
    }

    private void LoadChart()
    {
        foreach (NoteParam note in notes)
        {
            note.noteState = NoteParam.NOTE_STATE.NONE;

            PlayContext.LastBeatTime = Mathf.Max((float)PlayContext.LastBeatTime, (float)(note.beatEndTime));
        }
    }
}

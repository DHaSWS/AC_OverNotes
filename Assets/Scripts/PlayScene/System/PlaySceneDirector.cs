using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OverNotes;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

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
        PlayContext.routine = PlayContext.Routine.FadeOut;
        PlayContext.playDspTime = 0.0d;
        PlayContext.lastBeatTime = 0.0d;
        PlayContext.displayTime = 0.0d;

        SystemData.nowTime = 0;
        SystemData.maxTime = 0;
        SystemData.PlayData.combo = 0;

        ResultData.Count = new int[]
        {
            0,
            0,
            0,
            0,
            0,
            0
        };
        ResultData.score = 0.0f;
        ResultData.maxCombo = 0;

        GuideMessage.guideLane1 = "";
        GuideMessage.guideLane2 = "";
        GuideMessage.guideLane3 = "";
        GuideMessage.guideLane4 = "";

        SystemData.PlayData.lanes = lanes;
        beatmapLoader.CheckBeatmap();

        notes = SystemData.GetChart().notes;

        LoadChart();

        Debug.Log(SystemData.GetChart().maxCombo);

        ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { PlayContext.routine = PlayContext.Routine.Ready; });
    }

    private void FixedUpdate()
    {
        foreach (NoteParam note in notes)
        {
            if (note.beatTime <= PlayContext.displayTime && note.noteState == NoteParam.NOTE_STATE.NONE)
            {
                noteFactory.CreateNote(note.beatTime, note.beatEndTime, note.column);
                note.noteState = NoteParam.NOTE_STATE.NORMAL;
            }
        }

        if (PlayContext.routine == PlayContext.Routine.FadeIn &&
            ONFade.Same(ONFade.State.Idle_FadeIn))
        {
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () =>
            {
                SceneManager.LoadScene("Scenes/ResultScene");
            });
        }

        if (SystemData.nowTime > PlayContext.lastBeatTime)
        {
            PlayContext.routine = PlayContext.Routine.Finish;
        }
    }

    private void LoadChart()
    {
        foreach (NoteParam note in notes)
        {
            note.noteState = NoteParam.NOTE_STATE.NONE;

            PlayContext.lastBeatTime = Mathf.Max((float)PlayContext.lastBeatTime, (float)(note.beatEndTime));
        }
    }
}

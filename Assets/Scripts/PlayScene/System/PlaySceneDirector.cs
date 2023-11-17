using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OverNotes;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

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
        SystemData.PlayData.lanes = lanes;
        beatmapLoader.CheckBeatmap();

        notes = SystemData.GetChart().notes;

        LoadChart();

        Debug.Log(SystemData.GetChart().maxCombo);

        FadeContext.SetFadeOut(this, 0.5f, fadeImage, () => { PlayContext.routine = PlayContext.Routine.Ready; });
    }

    private void FixedUpdate()
    {
        foreach (NoteParam note in notes)
        {
            if(note.beatTime <= PlayContext.displayTime && note.noteState == NoteParam.NOTE_STATE.NONE)
            {
                note.noteState = NoteParam.NOTE_STATE.NORMAL;

                noteFactory.CreateNote(note.beatTime, note.beatEndTime, note.column);
            }
        }

        if (SystemData.nowTime > PlayContext.lastBeatTime)
        {
            PlayContext.routine = PlayContext.Routine.Finish;
        }

        if(PlayContext.routine== PlayContext.Routine.FadeIn)
        {
            FadeContext.SetFadeOut(this, 0.5f, fadeImage, () => { Debug.Log("おわったよ"); });
        }
    }

    private void LoadChart()
    {
        //List<NoteParam> notes = SystemData.GetChart().notes;
        foreach (NoteParam note in notes)
        {
            //noteFactory.CreateNote(note.beatTime, note.beatEndTime, note.column);

            PlayContext.lastBeatTime = Mathf.Max((float)PlayContext.lastBeatTime, (float)(note.beatEndTime));
        }
    }
}

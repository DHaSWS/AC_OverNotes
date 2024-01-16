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
    [SerializeField] Transform[] lanes;
    [SerializeField] NoteManager _noteManager;
    [SerializeField] NoteFactory noteFactory;
    [SerializeField] Image fadeImage;

    List<NoteParam> notes;

    private void Awake()
    {
        OnAwakeParams();
        OnAwakeLanes();
        _noteManager.OnAwake();

        ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { PlayContext.Routine = PlayContext.PlayRoutine.Ready; });
    }

    // On awake params
    private void OnAwakeParams() {
        PlayContext.Routine = PlayContext.PlayRoutine.FadeOut;
        PlayContext.PlayDspTime = 0.0d;
        PlayContext.LastBeatTime = 0.0d;
        PlayContext.DisplayTime = 0.0d;

        OverNotesSystem.Instance.NowTime = 0;
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
    }

    // On awake lanes
    private void OnAwakeLanes() {
        GuideMessage.GuideLane1 = "";
        GuideMessage.GuideLane2 = "";
        GuideMessage.GuideLane3 = "";
        GuideMessage.GuideLane4 = "";

        PlayData.Lanes = lanes;
    }

    private void FixedUpdate()
    {
        _noteManager.OnUpdate();

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
}

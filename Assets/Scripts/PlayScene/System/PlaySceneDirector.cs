using System.Collections.Generic;
using UnityEngine;
using OverNotes;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using OverNotes.System;

public class PlaySceneDirector : MonoBehaviour
{
    [SerializeField] private NoteManager _noteManager;
    [SerializeField] Image fadeImage;
    [SerializeField] Transform[] lanes;

    private void Awake()
    {
        OnAwakeParam();
        OnAwakeLanes();

        ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { PlayContext.Routine = PlayContext.PlayRoutine.Ready; });
    }

    /// <summary>
    /// Awake param
    /// </summary>
    private void OnAwakeParam() {
        PlayContext.Routine = PlayContext.PlayRoutine.FadeOut;
        PlayContext.PlayDspTime = 0.0d;
        PlayContext.LastBeatTime = 0.0d;
        PlayContext.DisplayTime = 0.0d;

        OverNotesSystem.Instance.NowTime = 0;

        ResultData.Count = new int[] { 0, 0, 0, 0, 0, 0 };
        ResultData.Score = 0.0f;
        ResultData.MaxCombo = 0;
        PlayData.Combo = 0;
    }

    /// <summary>
    /// Awake lanes
    /// </summary>
    private void OnAwakeLanes() {
        GuideMessage.GuideLane1 = "";
        GuideMessage.GuideLane2 = "";
        GuideMessage.GuideLane3 = "";
        GuideMessage.GuideLane4 = "";

        PlayData.Lanes = lanes;
    }

    private void FixedUpdate()
    {
        OnUpdateRoutine();
    }

    /// <summary>
    /// Update routine
    /// </summary>
    private void OnUpdateRoutine() {
        if (PlayContext.Routine == PlayContext.PlayRoutine.FadeIn &&
            ONFade.Same(ONFade.State.Idle_FadeIn)) {
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () => {
                SceneManager.LoadScene("Scenes/ResultScene");
            });
        }

        if (OverNotesSystem.Instance.NowTime > PlayContext.LastBeatTime) {
            PlayContext.Routine = PlayContext.PlayRoutine.Finish;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverNotes;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using OverNotes.System;

public class ResultDirector : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] private Text score;
    [SerializeField] private Text acc;
    [SerializeField] private Text perfectP;
    [SerializeField] private Text perfect;
    [SerializeField] private Text great;
    [SerializeField] private Text good;
    [SerializeField] private Text bad;
    [SerializeField] private Text miss;
    [SerializeField] private Text title;
    [SerializeField] private Text artist;
    [SerializeField] private Text chart;

    private void Start()
    {
        OverNotesSystem system = OverNotesSystem.Instance;
        
        ResultContext.state = ResultContext.State.Fade_Out;

        GuideMessage.GuideLane1 = "";
        GuideMessage.GuideLane2 = "";
        GuideMessage.GuideLane3 = "ŽŸ‚Ö";
        GuideMessage.GuideLane4 = "";

        score.text = Mathf.RoundToInt(ResultData.Score).ToString("N0");
        acc.text = (ResultData.Score / SystemConstants.AllPerfectScore).ToString("000.00%");
        perfectP.text = ResultData.Count[0].ToString();
        perfect.text = ResultData.Count[1].ToString();
        great.text = ResultData.Count[2].ToString();
        good.text = ResultData.Count[3].ToString();
        bad.text = ResultData.Count[4].ToString();
        miss.text = ResultData.Count[5].ToString();

        title.text = system.GetBeatmap().title;
        artist.text = system.GetBeatmap().artist;
        chart.text = system.GetChart().diffucult.ToString() + " " + system.GetChart().level;

        ONFade.SetFadeOut(this, 0.5f, fadeImage, () =>
        {
            ResultContext.state = ResultContext.State.Intro;
        });
    }

    private void Update()
    {
        if (ResultContext.state == ResultContext.State.Intro &&
            animator.GetFloat("Speed") == 0.0f)
        {
            animator.SetFloat("Speed", 1.0f);
        }
    }

    public void SetState(ResultContext.State state)
    {
        if(ResultContext.state != state)
        {
            ResultContext.state = state;
        }
    }

    public void TriggeredKey(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if(ResultContext.state != ResultContext.State.Fade_In)
        {
            ResultContext.state = ResultContext.State.Fade_In;
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () =>
            {
                SceneManager.LoadScene("Scenes/SelectScene");
            });
        }
    }
}

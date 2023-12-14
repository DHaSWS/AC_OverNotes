using OverNotes;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using OverNotes.System;

public class SelectSceneDirector : MonoBehaviour
{
    [SerializeField] private Animator songAnimator;
    [SerializeField] private Animator settingAnimator;
    [SerializeField] private Text noteSpeed;
    [SerializeField] private GameObject chartList;
    [SerializeField] private Image fadeImage;
    [SerializeField] private BeatmapLoader beatmapLoader;
    [Space] // ----------------------------------------------------------------
    [Header("Setting")]
    [SerializeField] private SettingPanelScrollView settingPanelScrollView;

    private void OnEnable()
    {
        SelectContext.selectRoutine = SelectContext.SelectRoutine.Song;

        OverNotesSystem.Instance.ChartIndex = 0;
        OverNotesSystem.Instance.SongIndex = 0;
        beatmapLoader.CheckBeatmap();

        GuideMessage.guideLane1 = "戻る";
        GuideMessage.guideLane2 = "決定";
        GuideMessage.guideLane3 = "前へ";
        GuideMessage.guideLane4 = "次へ";

        ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
    }

    private void Update()
    {
        if (SelectContext.selectRoutine == SelectContext.SelectRoutine.Song &&
            chartList.activeSelf)
        {
            chartList.SetActive(false);
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        Debug.Log("Select");

        OverNotesSystem system = OverNotesSystem.Instance;

        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Song:
                {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Song_Select;
                    songAnimator.SetInteger("MoveRoutine", 1);
                    chartList.SetActive(true);
                }
                break;
            case SelectContext.SelectRoutine.Chart:
                {
                    // Settingだったら
                    if(system.ChartIndex == system.GetBeatmap().charts.Count) {
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;
                        // Fade out
                        SettingPanelParams.IsFadeIn = false;
                    } else {
                        // フェードインを行う
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.FadeIn;
                        ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/PlayScene"); });
                    }
                    break;
                }
            case SelectContext.SelectRoutine.Setting: {
                    if(settingPanelScrollView != null) {
                        settingPanelScrollView.Select();
                    }
                    break;
                }
        }
    }
    public void Back(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        Debug.Log("Back");

        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Chart:
                {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart_Back;
                    songAnimator.SetInteger("MoveRoutine", 2);
                }
                break;
            case SelectContext.SelectRoutine.Setting:
                {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart;
                    // Fade in
                    SettingPanelParams.IsFadeIn = true;
                }
                break;
            case SelectContext.SelectRoutine.Setting_Value: {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;
                    break;
                }
        }
    }

    public void AddValue(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        OverNotesSystem system = OverNotesSystem.Instance;
        
        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Setting:
                {
                    break;
                }
        }
    }
    public void SubValue(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        OverNotesSystem system = OverNotesSystem.Instance;

        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Setting:
                {
                    break;
                }
        }
    }
}

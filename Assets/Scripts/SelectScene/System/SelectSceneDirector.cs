using OverNotes;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneDirector : MonoBehaviour
{
    [SerializeField] private Animator songAnimator;
    [SerializeField] private Animator settingAnimator;
    [SerializeField] private Text noteSpeed;
    [SerializeField] private GameObject chartList;
    [SerializeField] private Image fadeImage;
    [SerializeField] private BeatmapLoader beatmapLoader;

    private void Awake()
    {
        SelectContext.selectRoutine = SelectContext.SelectRoutine.Song;
        OverNotes.SystemData.songIndex = 0;
        OverNotes.SystemData.chartIndex = 0;

        GuideMessage.guideLane1 = "戻る";
        GuideMessage.guideLane2 = "決定";
        GuideMessage.guideLane3 = "前へ";
        GuideMessage.guideLane4 = "次へ";

        beatmapLoader.CheckBeatmap();
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
                    if(OverNotes.SystemData.chartIndex == OverNotes.SystemData.GetBeatmap().charts.Count) {
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting;
                        settingAnimator.SetFloat("Speed", 1.0f);
                        noteSpeed.text = OverNotes.SystemData.noteSpeed.ToString();
                    } else {
                        // フェードインを行う
                        SelectContext.selectRoutine = SelectContext.SelectRoutine.FadeIn;
                        ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/PlayScene"); });
                    }
                }
                break;
            case SelectContext.SelectRoutine.Setting:
                {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.FadeIn;
                    ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/PlayScene"); });
                }
                break;
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
                    settingAnimator.SetFloat("Speed", -1.0f);
                    GuideMessage.guideLane3 = "前へ";
                    GuideMessage.guideLane4 = "次へ";
                }
                break;
        }
    }

    public void AddValue(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Setting:
                {
                    OverNotes.SystemData.noteSpeed += 0.5f;
                    OverNotes.SystemData.noteSpeed = Mathf.Clamp(OverNotes.SystemData.noteSpeed, 1.0f, 30.0f);
                    noteSpeed.text = OverNotes.SystemData.noteSpeed.ToString();

                    break;
                }
        }
    }
    public void SubValue(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Setting:
                {
                    OverNotes.SystemData.noteSpeed -= 0.5f;
                    OverNotes.SystemData.noteSpeed = Mathf.Clamp(OverNotes.SystemData.noteSpeed, 1.0f, 30.0f);
                    noteSpeed.text = OverNotes.SystemData.noteSpeed.ToString();

                    break;
                }
        }
    }
}

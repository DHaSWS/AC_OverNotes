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
        SystemData.songIndex = 0;
        SystemData.chartIndex = 0;

        GuideMessage.guideLane1 = "–ß‚é";
        GuideMessage.guideLane2 = "Œˆ’è";
        GuideMessage.guideLane3 = "‘O‚Ö";
        GuideMessage.guideLane4 = "ŽŸ‚Ö";

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
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting_Speed;
                    settingAnimator.SetFloat("Speed", 1.0f);
                    GuideMessage.guideLane3 = "Œ¸‚ç‚·";
                    GuideMessage.guideLane4 = "‘‚â‚·";
                    noteSpeed.text = SystemData.noteSpeed.ToString();
                }
                break;
            case SelectContext.SelectRoutine.Setting_Speed:
                {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Setting_Speed_Select;
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
            case SelectContext.SelectRoutine.Setting_Speed:
                {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart;
                    settingAnimator.SetFloat("Speed", -1.0f);
                    GuideMessage.guideLane3 = "‘O‚Ö";
                    GuideMessage.guideLane4 = "ŽŸ‚Ö";
                }
                break;
        }
    }

    public void AddValue(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Setting_Speed:
                {
                    SystemData.noteSpeed += 0.5f;
                    SystemData.noteSpeed = Mathf.Clamp(SystemData.noteSpeed, 1.0f, 30.0f);
                    noteSpeed.text = SystemData.noteSpeed.ToString();

                    break;
                }
        }
    }
    public void SubValue(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        switch (SelectContext.selectRoutine)
        {
            case SelectContext.SelectRoutine.Setting_Speed:
                {
                    SystemData.noteSpeed -= 0.5f;
                    SystemData.noteSpeed = Mathf.Clamp(SystemData.noteSpeed, 1.0f, 30.0f);
                    noteSpeed.text = SystemData.noteSpeed.ToString();

                    break;
                }
        }
    }
}

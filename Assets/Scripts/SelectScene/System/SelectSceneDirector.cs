using OverNotes;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneDirector : MonoBehaviour
{
    [SerializeField] private Animator songAnimator;
    [SerializeField] private GameObject chartList;
    [SerializeField] private Image fadeImage;
    [SerializeField] private BeatmapLoader beatmapLoader;

    private void Awake()
    {
        beatmapLoader.CheckBeatmap();
    }

    private void Update()
    {
        if(SelectContext.selectRoutine == SelectContext.SelectRoutine.Song &&
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
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart_Select;
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
        }
    }
}

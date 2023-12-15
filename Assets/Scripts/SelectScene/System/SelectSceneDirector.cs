using OverNotes;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using OverNotes.System;
using System;

public class SelectSceneDirector : MonoBehaviour {
    [SerializeField] private Animator songAnimator;
    [SerializeField] private Animator settingAnimator;
    [SerializeField] private Text noteSpeed;
    [SerializeField] private GameObject chartList;
    [SerializeField] private Image fadeImage;
    [Space] // ----------------------------------------------------------------
    [Header("Setting")]
    [SerializeField] private SettingPanelScrollView settingPanelScrollView;

    private void Start() {
        SelectContext.selectRoutine = SelectContext.SelectRoutine.Song;

        OverNotesSystem.Instance.ChartIndex = 0;
        OverNotesSystem.Instance.SongIndex = 0;

        GuideMessage.GuideLane1 = "戻る";
        GuideMessage.GuideLane2 = "決定";
        GuideMessage.GuideLane3 = "前へ";
        GuideMessage.GuideLane4 = "次へ";

        ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
    }

    private void Update() {
        if (SelectContext.selectRoutine == SelectContext.SelectRoutine.Song &&
            chartList.activeSelf) {
            chartList.SetActive(false);
        }
    }

    public void Select(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }

        OverNotesSystem system = OverNotesSystem.Instance;

        switch (SelectContext.selectRoutine) {
            case SelectContext.SelectRoutine.Song: {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Song_Select;
                    songAnimator.SetInteger("MoveRoutine", 1);
                    chartList.SetActive(true);
                }
                break;
            case SelectContext.SelectRoutine.Chart: {
                    // Settingだったら
                    if (system.ChartIndex == system.GetBeatmap().charts.Count) {
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
            case SelectContext.SelectRoutine routine when
                routine == SelectContext.SelectRoutine.Setting ||
                routine == SelectContext.SelectRoutine.Setting_Value: {
                    if (settingPanelScrollView != null) {
                        settingPanelScrollView.Select();
                    }
                    break;
                }
        }
    }

    public void Back(InputAction.CallbackContext context) {
        if (!context.started) {
            return;
        }

        switch (SelectContext.selectRoutine) {
            case SelectContext.SelectRoutine.Chart: {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart_Back;
                    songAnimator.SetInteger("MoveRoutine", 2);
                }
                break;
            case SelectContext.SelectRoutine.Setting: {
                    SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart;
                    // Fade in
                    SettingPanelParams.IsFadeIn = true;
                }
                break;
            case SelectContext.SelectRoutine.Setting_Value: {
                    if (settingPanelScrollView != null) {
                        settingPanelScrollView.Back();
                    }
                    break;
                }
        }
    }

    public void AddValue(InputAction.CallbackContext context) {
        if (!context.started) return;

        OverNotesSystem system = OverNotesSystem.Instance;

        switch (SelectContext.selectRoutine) {
            case SelectContext.SelectRoutine.Setting_Value: {
                    if (settingPanelScrollView != null) {
                        settingPanelScrollView.AddValue();
                    }
                    break;
                }
        }
    }
    public void SubValue(InputAction.CallbackContext context) {
        if (!context.started) return;

        OverNotesSystem system = OverNotesSystem.Instance;

        switch (SelectContext.selectRoutine) {
            case SelectContext.SelectRoutine.Setting_Value: {
                    if (settingPanelScrollView != null) {
                        settingPanelScrollView.SubValue();
                    }
                    break;
                }
        }
    }
}

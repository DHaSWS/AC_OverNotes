using EasingCore;
using FancyScrollView;
using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingPanelScrollView : FancyScrollView<SettingPanelItemData>
{
    [SerializeField] private Scroller scroller = default;
    [SerializeField] private GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;

    private void Start() {
        scroller.OnValueChanged(base.UpdatePosition);
    }

    protected override void Initialize() {
        base.Initialize();

        scroller.OnValueChanged(UpdatePosition);
        scroller.OnSelectionChanged(UpdateSelection);
    }

    public void UpdateData(IList<SettingPanelItemData> items) {
        base.UpdateContents(items);
        scroller.SetTotalCount(items.Count);
    }

    void UpdateSelection(int index) {
        if(SettingPanelParams.SettingIndex == index) {
            return;
        }
        SettingPanelParams.SettingIndex = index;
        Refresh();
    }

    public void Select() {
        OverNotesSystem system = OverNotesSystem.Instance;
        if(SelectContext.selectRoutine == SelectContext.SelectRoutine.Setting) {
            system.settingItems[SettingPanelParams.SettingIndex].Select();
        }
    }

    public void SelectCell(int index) {
        if (SelectContext.selectRoutine != SelectContext.SelectRoutine.Setting || index == SettingPanelParams.SettingIndex) {
            return;
        }
        Debug.Log("Setting:SelectCell");
        UpdateSelection(index);
        scroller.ScrollTo(index, 0.5f, Ease.OutCubic);
    }

    public void SelectNextCell(InputAction.CallbackContext context) {
        if (!context.started) return;
        SelectCell(SettingPanelParams.SettingIndex + 1);
    }

    public void SelectPrevCell(InputAction.CallbackContext context) {
        if (!context.started) return;

        SelectCell(SettingPanelParams.SettingIndex - 1);
    }
}

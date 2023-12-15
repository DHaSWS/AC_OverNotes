using UnityEngine;
using System.Linq;
using FancyScrollView;
using System.Collections.Generic;
using OverNotes;
using EasingCore;
using UnityEngine.InputSystem;
using OverNotes.System;

class ChartScrollView : FancyScrollView<Chart_ItemData>
{
    [SerializeField] Scroller scroller = default;
    [SerializeField] GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;

    void Start()
    {
        scroller.OnValueChanged(base.UpdatePosition);
    }

    protected override void Initialize()
    {
        base.Initialize();

        scroller.OnValueChanged(UpdatePosition);
        scroller.OnSelectionChanged(UpdateSelection);
    }

    public void UpdateData(IList<Chart_ItemData> items)
    {
        base.UpdateContents(items);
        scroller.SetTotalCount(items.Count);
    }

    void UpdateSelection(int index)
    {
        OverNotesSystem system = OverNotesSystem.Instance;

        if (system.ChartIndex == index)
        {
            return;
        }

        system.ChartIndex = index;
        Refresh();
    }

    public void SelectNextCell(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        SelectCell(OverNotesSystem.Instance.ChartIndex + 1);
    }

    public void SelectPrevCell(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        SelectCell(OverNotesSystem.Instance.ChartIndex - 1);
    }

    public void SelectCell(int index)
    {
        if (SelectContext.selectRoutine != SelectContext.SelectRoutine.Chart || index == OverNotesSystem.Instance.ChartIndex)
        {
            return;
        }

        UpdateSelection(index);
        scroller.ScrollTo(index, 0.5f, Ease.OutCubic);
    }

}
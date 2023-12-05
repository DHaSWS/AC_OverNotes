using UnityEngine;
using System.Linq;
using FancyScrollView;
using System.Collections.Generic;
using OverNotes;
using EasingCore;
using UnityEngine.InputSystem;

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
        if (OverNotes.SystemData.chartIndex == index)
        {
            return;
        }

        OverNotes.SystemData.chartIndex = index;
        Refresh();
    }

    public void SelectNextCell(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Debug.Log("SelectNextCell");
        SelectCell(OverNotes.SystemData.chartIndex + 1);
    }

    public void SelectPrevCell(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Debug.Log("SelectPrevCell");
        SelectCell(OverNotes.SystemData.chartIndex - 1);
    }

    public void SelectCell(int index)
    {
        if (SelectContext.selectRoutine != SelectContext.SelectRoutine.Chart || index == OverNotes.SystemData.chartIndex)
        {
            return;
        }

        UpdateSelection(index);
        scroller.ScrollTo(index, 0.5f, Ease.OutCubic);
    }

}
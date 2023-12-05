using UnityEngine;
using System.Linq;
using FancyScrollView;
using System.Collections.Generic;
using OverNotes;
using EasingCore;
using UnityEngine.InputSystem;

class SongScrollView : FancyScrollView<ItemData>
{
    [SerializeField] Scroller scroller = default;
    [SerializeField] GameObject cellPrefab = default;
    [SerializeField] private int prevIndex = 0;

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

    public void UpdateData(IList<ItemData> items)
    {
        base.UpdateContents(items);
        scroller.SetTotalCount(items.Count);
    }

    void UpdateSelection(int index)
    {
        if (OverNotes.SystemData.songIndex == index)
        {
            return;
        }

        OverNotes.SystemData.songIndex = index;

        prevIndex = index;
        Refresh();
    }

    public void SelectNextCell(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Debug.Log("SelectNextCell");
        SelectCell(OverNotes.SystemData.songIndex + 1);
    }

    public void SelectPrevCell(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Debug.Log("SelectPrevCell");
        SelectCell(OverNotes.SystemData.songIndex - 1);
    }

    public void SelectCell(int index)
    {
        if (SelectContext.selectRoutine != SelectContext.SelectRoutine.Song || index == OverNotes.SystemData.songIndex)
        {
            return;
        }

        int clampedIndex = Mathf.Clamp(index, 0, OverNotes.SystemData.beatmaps.Count - 1);

        Debug.Log(clampedIndex);
        UpdateSelection(clampedIndex);
        scroller.ScrollTo(clampedIndex, 0.5f, Ease.OutCubic);
    }

}
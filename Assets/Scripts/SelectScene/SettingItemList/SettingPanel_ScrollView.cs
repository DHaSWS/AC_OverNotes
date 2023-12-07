using FancyScrollView;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SettingPanel_ScrollView : FancyScrollView<SettingPanel_ItemData>
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

    public void UpdateData(IList<SettingPanel_ItemData> items) {
        base.UpdateContents(items);
        scroller.SetTotalCount(items.Count);
    }

    void UpdateSelection(int index) {

    }
}

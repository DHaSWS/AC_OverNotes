using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelItemData {
    public SettingItem Item { get; }
    public int Index { get; }

    public SettingPanelItemData(SettingItem item, int index) {
        Item = item;
        Index = index;
    }
}

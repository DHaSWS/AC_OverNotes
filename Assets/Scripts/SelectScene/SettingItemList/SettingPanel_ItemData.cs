using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel_ItemData {
    public string ItemText { get; }
    public string ItemValue { get; }
    public string ItemGuide { get; }

    public SettingPanel_ItemData(string itemText, string itemValue, string itemGuide) {
        ItemText = itemText;
        ItemValue = itemValue;
        ItemGuide = itemGuide;
    }
}

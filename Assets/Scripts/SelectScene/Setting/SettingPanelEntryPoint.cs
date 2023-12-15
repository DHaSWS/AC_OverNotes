using OverNotes.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using UnityEngine;

public class SettingPanelEntryPoint : MonoBehaviour
{
    [SerializeField] SettingPanelScrollView scrollView = default;

    private void Start() {
        int index = SettingPanelParams.SettingIndex;

        OverNotesSystem system = OverNotesSystem.Instance;

        // ここは設定のやつ
        int count = system.SettingItems.Count;

        // ここでリスト作る
        List<SettingPanelItemData> items = Enumerable.Range(0, count)
            .Select(i => new SettingPanelItemData(system.SettingItems[i], i)
            ).ToList();

        //scrollView.UpdateData();でリストを入れる
        scrollView.UpdateData(items);
    }
}

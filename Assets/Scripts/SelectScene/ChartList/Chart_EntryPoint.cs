using UnityEngine;
using OverNotes;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

class Chart_EntryPoint : MonoBehaviour
{
    [SerializeField] ChartScrollView myScrollView = default;

    private void OnEnable()
    {
        Debug.Log("Enabled");

        OverNotesSystem system = OverNotesSystem.Instance;

        int index = system.SongIndex;

        BeatmapData beatmapData = system.Beatmaps[index];

        List<ChartInfo> chartInfo = beatmapData.charts;
        int count = chartInfo.Count;

        List<Chart_ItemData> items = Enumerable.Range(0, count)
            .Select(i => new Chart_ItemData(chartInfo[i].diffucult.ToString(), chartInfo[i].level.ToString()))
            .ToList();
        items.Add(new Chart_ItemData("Setting", ""));

        myScrollView.UpdateData(items);
    }
}
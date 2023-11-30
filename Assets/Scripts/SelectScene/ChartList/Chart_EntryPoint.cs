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

        int index = SystemData.songIndex;

        BeatmapData beatmapData = SystemData.beatmaps[index];

        List<ChartInfo> chartInfo = beatmapData.charts;

        int count = chartInfo.Count;

        var items = Enumerable.Range(0, count)
            .Select(i => new Chart_ItemData(chartInfo[i].diffucult.ToString(), chartInfo[i].level.ToString()))
            .ToArray();

        myScrollView.UpdateData(items);
    }
}
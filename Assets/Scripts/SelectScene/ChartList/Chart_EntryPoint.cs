using UnityEngine;
using OverNotes;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

class Chart_EntryPoint : MonoBehaviour
{
    [SerializeField] ChartScrollView myScrollView = default;
    [SerializeField] Text textTitle;
    [SerializeField] Text textArtist;

    private void OnEnable()
    {
        Debug.Log("Enabled");

        int index = SystemData.songIndex;

        BeatmapData beatmapData = SystemData.beatmaps[index];

        List<ChartInfo> chartInfo = beatmapData.charts;

        int count = chartInfo.Count;

        textTitle.text = beatmapData.title;
        textArtist.text = beatmapData.artist;

        var items = Enumerable.Range(0, count)
            .Select(i => new Chart_ItemData(chartInfo[i].diffucult.ToString(), chartInfo[i].level.ToString()))
            .ToArray();

        myScrollView.UpdateData(items);
    }
}
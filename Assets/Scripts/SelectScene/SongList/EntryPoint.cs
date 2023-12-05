using UnityEngine;
using OverNotes;
using System.Linq;
using UnityEngine.UI;
using System.Drawing.Imaging;

class EntryPoint : MonoBehaviour
{
    [SerializeField] SongScrollView myScrollView = default;

    void Start()
    {
        int count = OverNotes.SystemData.beatmaps.Count;
        var items = Enumerable.Range(0, count)
            .Select(i => new ItemData(SystemData.beatmaps[i].title, SystemData.beatmaps[i].artist))
            .ToArray();

        myScrollView.UpdateData(items);
    }
}
using UnityEngine;
using OverNotes;
using System.Linq;

class EntryPoint : MonoBehaviour
{
    [SerializeField] SongScrollView myScrollView = default;

    void Start()
    {
        int count = SystemData.beatmaps.Count;
        var items = Enumerable.Range(0, count)
            .Select(i => new ItemData(SystemData.beatmaps[i].title))
            .ToArray();

        myScrollView.UpdateData(items);
    }
}
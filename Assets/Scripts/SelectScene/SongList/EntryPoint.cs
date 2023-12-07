using UnityEngine;
using OverNotes;
using OverNotes.System;
using System.Linq;
using UnityEngine.UI;
using System.Drawing.Imaging;

class EntryPoint : MonoBehaviour
{
    [SerializeField] SongScrollView myScrollView = default;

    void Start()
    {
        OverNotesSystem system = OverNotesSystem.Instance;

        int count = system.Beatmaps.Count;
        var items = Enumerable.Range(0, count)
            .Select(i => new ItemData(system.Beatmaps[i].title, system.Beatmaps[i].artist))
            .ToArray();

        myScrollView.UpdateData(items);
    }
}
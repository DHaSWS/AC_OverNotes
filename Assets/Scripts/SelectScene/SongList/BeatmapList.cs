using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverNotes;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class BeatmapList : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void AnimEndSongSelect()
    {
        Debug.Log("AnimEndSongSelect");
        SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart;
    }
    public void AnimEndChartBack()
    {
        Debug.Log("AnimEndChartBack");
        animator.SetInteger("MoveRoutine", 0);
        SelectContext.selectRoutine = SelectContext.SelectRoutine.Song;
    }
    public void AnimStart(string str)
    {
        Debug.Log(str);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverNotes;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ChartList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AnimEndSongSelect()
    {
        //Debug.Log("AnimEndSongSelect");
        //SelectContext.selectRoutine = SelectContext.SelectRoutine.Chart;
    }
    public void AnimEndChartBack()
    {
        //Debug.Log("AnimEndChartBack");
        //SelectContext.selectRoutine = SelectContext.SelectRoutine.Song;
    }
    public void AnimStart(string str)
    {
        Debug.Log(str);
    }
}

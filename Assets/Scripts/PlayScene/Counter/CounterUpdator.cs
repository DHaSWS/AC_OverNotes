using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterUpdator : MonoBehaviour
{
    [SerializeField] Text[] judgeCounter;
    [SerializeField] Text maxCombo;
    [SerializeField] Text combo;

    // Start is called before the first frame update
    void Start()
    {
        if(judgeCounter.Length != 6)
        {
            Debug.LogError("数があわないよ");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i =  0; i < judgeCounter.Length; i++)
        {
            judgeCounter[i].text = ResultData.Count[i].ToString();
        }

        combo.text = OverNotes.SystemData.PlayData.combo.ToString();

        if(OverNotes.SystemData.PlayData.combo > ResultData.maxCombo)
        {
            ResultData.maxCombo = OverNotes.SystemData.PlayData.combo;
        }
        maxCombo.text = ResultData.maxCombo.ToString();
    }
}

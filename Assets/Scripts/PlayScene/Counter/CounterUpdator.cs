using OverNotes;
using OverNotes.System;
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
            Debug.LogError("The numbers don't add up.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i =  0; i < judgeCounter.Length; i++)
        {
            judgeCounter[i].text = ResultData.Count[i].ToString();
        }

        combo.text = PlayData.Combo.ToString();

        if(PlayData.Combo > ResultData.MaxCombo)
        {
            ResultData.MaxCombo = PlayData.Combo;
        }
        maxCombo.text = ResultData.MaxCombo.ToString();
    }
}

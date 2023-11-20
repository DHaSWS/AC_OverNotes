using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGuide : MonoBehaviour
{
    [SerializeField] private Text guideLane1;
    [SerializeField] private Text guideLane2;
    [SerializeField] private Text guideLane3;
    [SerializeField] private Text guideLane4;
    void Update()
    {
        guideLane1.text = GuideMessage.guideLane1;
        guideLane2.text = GuideMessage.guideLane2;
        guideLane3.text = GuideMessage.guideLane3;
        guideLane4.text = GuideMessage.guideLane4;
    }
}

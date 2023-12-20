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

    [SerializeField] private Text[] _guideLanes = new Text[4];

    void Update()
    {
        for(int i = 0; i < _guideLanes.Length; i++) {
            if (_guideLanes[i].text != GuideMessage.GuideLanes[i]) {
                _guideLanes[i].text = GuideMessage.GuideLanes[i];
            }
        }
    }
}

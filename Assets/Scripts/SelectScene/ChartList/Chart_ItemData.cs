using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Chart_ItemData
{
    public string m_difficult { get; }
    public string m_level { get; }

    public Chart_ItemData(string diffucult, string level) 
    {
        m_difficult = diffucult;
        m_level = level;
    }
}

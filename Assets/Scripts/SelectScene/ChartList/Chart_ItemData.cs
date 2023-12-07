using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Chart_ItemData
{
    public string m_text { get; }
    public string m_level { get; }

    public Chart_ItemData(string text, string level) 
    {
        m_text = text;
        m_level = level;
    }
}

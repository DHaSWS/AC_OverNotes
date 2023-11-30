using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemData
{
    public string m_title { get; }
    public string m_artist { get; }

    public ItemData(string title, string artist) 
    {
        m_title = title;
        m_artist = artist;
    }
}

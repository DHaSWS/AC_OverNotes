using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneCover : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        float maxHeight = 9.5f;
        float settingHeight = (float)OverNotesSystem.Instance.SettingItems[(int)SystemConstants.SettingItemTag.LaneCoverSize].GetValue();
        float height = maxHeight * (settingHeight / 100.0f);

        Vector3 position = transform.localPosition;
        position.y = 4 - height / 2;
        transform.localPosition = position;

        Vector3 scale = transform.localScale;
        scale.y = height;
        transform.localScale = scale;
    }
}

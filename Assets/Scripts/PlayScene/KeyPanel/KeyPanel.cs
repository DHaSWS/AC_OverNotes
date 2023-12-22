using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanel : MonoBehaviour {
    [SerializeField] private Text[] _bindTexts = new Text[4];
    private void Awake() {
        for(int i = 0; i < 4; i++) {
            _bindTexts[i].text = KeyManager.Instance.KeyBindTexts[i].text;
        }

        KeyManager.Instance.GetComponent<CanvasGroup>().alpha = 0.0f;
    }
}

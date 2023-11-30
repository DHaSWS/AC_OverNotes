using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class PauseScreen : MonoBehaviour
{
    static float COUNT_DOWN_TIME_S = 4.0f;

    [SerializeField] Canvas pauseCanvas;
    [SerializeField] TextMeshProUGUI retry;
    [SerializeField] TextMeshProUGUI backGoTitile;
    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] Image backScreen;
    private bool isCountDown = false;
    private float countDownTimer_s = COUNT_DOWN_TIME_S;

    // Start is called before the first frame update
    void Start()
    {
        countDown.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // 現在のキーボード情報
        var current = Keyboard.current;

        // キーボード接続チェック
        if (current == null)
        {
            return;
        }

        var ESCKey = current[Key.Escape];

        if(ESCKey.wasPressedThisFrame)
        {
            if(pauseCanvas.sortingOrder < 0)
            {
                pauseCanvas.sortingOrder = 10000;
                Time.timeScale = 0;
                retry.enabled = true;
                backGoTitile.enabled = true;
                backScreen.enabled = true;
                countDown.enabled = false;
                Debug.Log("pause");
            }
            else if (pauseCanvas.sortingOrder > 0)
            {
                retry.enabled = false;
                backGoTitile.enabled = false;
                countDown.enabled = true;
                backScreen.enabled = false;
                countDownTimer_s = COUNT_DOWN_TIME_S;
                Debug.Log("notpause");

                isCountDown = true;
            }
        }

        countDownTimer_s -= 1.0f / 120.0f * (float)Convert.ToDouble(isCountDown);
        //countDownTimer_s -= (COUNT_DOWN_TIME_S / 60) * Convert.ToInt32(isCountDown);
        countDown.text = ((int)countDownTimer_s).ToString();


        if ((int)countDownTimer_s < 0.0f)
        {
            Time.timeScale = 1;
            pauseCanvas.sortingOrder = -1;
            countDownTimer_s = COUNT_DOWN_TIME_S;
            isCountDown = false;
            countDown.enabled = false;
        }

    }
}

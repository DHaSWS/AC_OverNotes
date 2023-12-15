using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDirctor : MonoBehaviour
{
	[SerializeField] private Image fadeImage;
	[SerializeField] private TextMeshProUGUI pushSpaceKey;
	private float feedColor = 1.0f;
	private int select = 0;

    [SerializeField] private AudioSource audio;
    private bool bgm = false;
	// Start is called before the first frame update
	void Start()
	{
		//フェード画像のフェードアウト
		ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
        audio.volume = 0.0f;
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

		var fKey = current[Key.F];
		var ESCKey = current[Key.Escape];
        var upKey = current[Key.UpArrow];
        var downKey = current[Key.DownArrow];

        if (upKey.wasPressedThisFrame)
		{
			select++;

        }
        else if (downKey.wasPressedThisFrame)
        {
			select--;
        }

        Math.Clamp(select, 0, 1);

        //スペースキーが押された場合フェードインし曲選択に移行
        //if (fKey.wasPressedThisFrame)
        //    ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
        if (bgm == true)
            audio.volume -= 0.02f;

        if (ESCKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
			    Application.Quit();//ゲームプレイ終了
#endif
        }

        if (pushSpaceKey.color.a >= 1 || pushSpaceKey.color.a <= 0)
			feedColor *= -1;

		pushSpaceKey.color = new Color(pushSpaceKey.color.r, pushSpaceKey.color.g, pushSpaceKey.color.b, pushSpaceKey.color.a + (feedColor * Time.deltaTime));

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //シーン遷移の入力を自由にさせる為に作りました。消す時は//で囲んだところを消してください。　
        audio.volume += 0.01f;
        Awake();
        OnDestroy();
        OnEnable();
        OnDisable();
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    private InputAction _anyKeyAction;

    private void Awake()
    {
        // 任意キーのAction作成
        _anyKeyAction = new InputAction(
            "AnyKey",
            InputActionType.Button,
            "<Keyboard>/anyKey"
        );

        // コールバック登録
        _anyKeyAction.performed += OnAnyKey;
    }

    private void OnDestroy()
    {
        // コールバック解除
        _anyKeyAction.performed -= OnAnyKey;

        // Actionの破棄
        _anyKeyAction.Dispose();
    }

    private void OnEnable()
    {
        // Actionの有効化
        _anyKeyAction.Enable();
    }

    private void OnDisable()
    {
        // Actionの無効化
        _anyKeyAction.Disable();
    }

    // 任意キーが押されたときの処理
    private void OnAnyKey(InputAction.CallbackContext context)
    {
        bgm = true;
        ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
}

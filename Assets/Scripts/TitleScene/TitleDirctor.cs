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
	[SerializeField] private TextMeshProUGUI pushAnyKey;
	private float feedColor = 1.0f;

    [SerializeField] private AudioSource bgm_audio = new AudioSource();
    private bool bgm = false;

    private InputAction _anyKeyAction;

	// Start is called before the first frame update
	void Start()
	{
		//フェード画像のフェードアウト
		ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
        bgm_audio.volume = 0.0f;
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

        //スペースキーが押された場合フェードインし曲選択に移行
        if (bgm == true)
            bgm_audio.volume -= 0.02f;

        if (ESCKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
			    Application.Quit();//ゲームプレイ終了
#endif
        }

        if (pushAnyKey.color.a >= 1 || pushAnyKey.color.a <= 0)
			feedColor *= -1;

		pushAnyKey.color = new Color(pushAnyKey.color.r, pushAnyKey.color.g, pushAnyKey.color.b, pushAnyKey.color.a + (feedColor * Time.deltaTime));

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //シーン遷移の入力を自由にさせる為に作りました。消す時は//で囲んだところを消してください。　
        bgm_audio.volume += 0.01f;
        
        Awake();
        OnDestroy();
        OnEnable();
        OnDisable();
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        if (SceneManager.GetActiveScene().name == "TitleScene")
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
}

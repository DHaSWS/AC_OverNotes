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
	// Start is called before the first frame update
	void Start()
	{
		//フェード画像のフェードアウト
		ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
		
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

		var spaceKey = current[Key.Space];
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
        if (spaceKey.wasPressedThisFrame)
			ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
		else if(ESCKey.wasPressedThisFrame )
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
		
	}
}

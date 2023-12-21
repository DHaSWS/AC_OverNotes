using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using OverNotes;
public class TitleDirctor : MonoBehaviour
{
	[SerializeField] private Image fadeImage;
	[SerializeField] private SpriteRenderer pushAnyKey;
	private float feedColor = 1.0f;

    [SerializeField] private AudioSource bgm_audio;

    // Start is called before the first frame update
    void Start()
	{
		//�t�F�[�h�摜�̃t�F�[�h�A�E�g
		ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
        bgm_audio.volume = 0.0f;

        KeyManager.Instance.StartRebinding();
    }
   
	// Update is called once per frame
	void Update()
	{
		// ���݂̃L�[�{�[�h���
		var current = Keyboard.current;

		// �L�[�{�[�h�ڑ��`�F�b�N
		if (current == null)
		{
			return;
        }

        var ESCKey = current[Key.Escape];

        if (ESCKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
			    Application.Quit();//�Q�[���v���C�I��
#endif
        }

        if (pushAnyKey.color.a >= 1 || pushAnyKey.color.a <= 0)
			feedColor *= -1;

        bgm_audio.volume += 0.01f;
        if(bgm_audio.volume>=0.4f)
        {
            bgm_audio.volume = 0.4f;
        }

        pushAnyKey.color = new Color(pushAnyKey.color.r, pushAnyKey.color.g, pushAnyKey.color.b, pushAnyKey.color.a + (feedColor * Time.deltaTime));
       
        for(int i=0;i<4;i++)
        {
            if (KeyManager.Instance.IsFinishedRebind[i] == true)
            {
                if (KeyManager.Instance.IsFinishedRebind[3]==true)
                {
                    bgm_audio.volume -= 0.001f;
                    if (SceneManager.GetActiveScene().name == "TitleScene")
                        ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
                }
            }
        }
    }
}

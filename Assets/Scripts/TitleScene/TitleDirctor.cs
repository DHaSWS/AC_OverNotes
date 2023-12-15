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
		//�t�F�[�h�摜�̃t�F�[�h�A�E�g
		ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
        bgm_audio.volume = 0.0f;
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

        //�X�y�[�X�L�[�������ꂽ�ꍇ�t�F�[�h�C�����ȑI���Ɉڍs
        if (bgm == true)
            bgm_audio.volume -= 0.02f;

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

		pushAnyKey.color = new Color(pushAnyKey.color.r, pushAnyKey.color.g, pushAnyKey.color.b, pushAnyKey.color.a + (feedColor * Time.deltaTime));

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //�V�[���J�ڂ̓��͂����R�ɂ�����ׂɍ��܂����B��������//�ň͂񂾂Ƃ���������Ă��������B�@
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
        // �C�ӃL�[��Action�쐬
        _anyKeyAction = new InputAction(
            "AnyKey",
            InputActionType.Button,
            "<Keyboard>/anyKey"
        );

        // �R�[���o�b�N�o�^
            _anyKeyAction.performed += OnAnyKey;
    }

    private void OnDestroy()
    {
        // �R�[���o�b�N����
            _anyKeyAction.performed -= OnAnyKey;

        // Action�̔j��
        _anyKeyAction.Dispose();
    }

    private void OnEnable()
    {
        // Action�̗L����
        _anyKeyAction.Enable();
    }

    private void OnDisable()
    {
        // Action�̖�����
        _anyKeyAction.Disable();
    }

    // �C�ӃL�[�������ꂽ�Ƃ��̏���
    private void OnAnyKey(InputAction.CallbackContext context)
    {
        bgm = true;
        if (SceneManager.GetActiveScene().name == "TitleScene")
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
}

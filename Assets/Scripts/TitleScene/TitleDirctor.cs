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

    [SerializeField] private AudioSource audio;

	// Start is called before the first frame update
	void Start()
	{
		//�t�F�[�h�摜�̃t�F�[�h�A�E�g
		ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
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

		var fKey = current[Key.F];
		var ESCKey = current[Key.Escape];
        

        //�X�y�[�X�L�[�������ꂽ�ꍇ�t�F�[�h�C�����ȑI���Ɉڍs
        if (fKey.wasPressedThisFrame)
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
        else if (ESCKey.wasPressedThisFrame)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
			    Application.Quit();//�Q�[���v���C�I��
#endif
        }

        if (pushSpaceKey.color.a >= 1 || pushSpaceKey.color.a <= 0)
			feedColor *= -1;

		pushSpaceKey.color = new Color(pushSpaceKey.color.r, pushSpaceKey.color.g, pushSpaceKey.color.b, pushSpaceKey.color.a + (feedColor * Time.deltaTime));

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //�V�[���J�ڂ̓��͂����R�ɂ�����ׂɍ��܂����B��������//�ň͂񂾂Ƃ���������Ă��������B�@
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
        ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
        
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
}

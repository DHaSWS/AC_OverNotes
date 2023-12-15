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
		//�t�F�[�h�摜�̃t�F�[�h�A�E�g
		ONFade.SetFadeOut(this, 0.5f, fadeImage, () => { });
        audio.volume = 0.0f;
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

        //�X�y�[�X�L�[�������ꂽ�ꍇ�t�F�[�h�C�����ȑI���Ɉڍs
        //if (fKey.wasPressedThisFrame)
        //    ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
        if (bgm == true)
            audio.volume -= 0.02f;

        if (ESCKey.wasPressedThisFrame)
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
        bgm = true;
        ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
}

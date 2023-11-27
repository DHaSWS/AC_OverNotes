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
    float feedColor = 0.01f;
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

        var spaceKey = current[Key.Space];

        //�t�F�[�h�摜�̃t�F�[�h�C��
        if (spaceKey.wasPressedThisFrame)
            ONFade.SetFadeIn(this, 0.5f, fadeImage, () => { SceneManager.LoadScene("Scenes/SelectScene"); });

        if (pushSpaceKey.color.a >= 1 || pushSpaceKey.color.a <= 0)
            feedColor *= -1;

        pushSpaceKey.color = new Color(pushSpaceKey.color.r, pushSpaceKey.color.g, pushSpaceKey.color.b, pushSpaceKey.color.a + feedColor);
        
    }
}

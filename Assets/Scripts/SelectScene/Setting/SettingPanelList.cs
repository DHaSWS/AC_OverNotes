using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelList : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    // Animator hash ----------------------------------------------------------
    static class AnimatorHash {
        public static readonly int Fade = Animator.StringToHash("SettingFadeAnimation");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_animator != null) {
            _animator.StartPlayback();
            _animator.Update(0);
            _animator.StopPlayback();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update animation
        UpdateAnimation();
    }

    /// <summary>
    /// Update animation
    /// </summary>
    private void UpdateAnimation() {
        AnimatorStateInfo alphaStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        float fadeSpeed = 0.0f;

        if (SettingPanelParams.IsFadeIn) {
            // Fade in
            if(alphaStateInfo.normalizedTime >= 0.0f) {
                fadeSpeed = -1.0f;
            }
        } else {
            // Fade out
            if (alphaStateInfo.normalizedTime <= 1.0f) {
                fadeSpeed = 1.0f;
            }
        }

        _animator.SetFloat("FadeSpeed", fadeSpeed);
    }
}

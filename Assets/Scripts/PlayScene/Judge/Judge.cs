using OverNotes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour {
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites = new Sprite[6];
    
    static class AnimatorHash {
        public static readonly int JudgeAnimation = Animator.StringToHash("Judge");
    }

    public void SetAnimation(PlayContext.Judge judge) {
        int address = (int)judge;
        _image.sprite = _sprites[address];
        _animator.Play(AnimatorHash.JudgeAnimation, 0, 0.0f);
    }
}

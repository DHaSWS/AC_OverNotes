using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour {
    [SerializeField] private Animator _animator;
    [SerializeField] private Sprite[] _sprites;
    
    static class AnimatorHash {
        public static readonly int Aniamtion = Animator.StringToHash("Judge");
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}

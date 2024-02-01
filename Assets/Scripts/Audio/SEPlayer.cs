using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SEPlayer : MonoBehaviour {
    [SerializeField] private AudioSource[] _sources;

    private void Start() {
        if (_sources == null) {
            throw new System.Exception("SourceÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç»Ç¢ÇÊ");
        }

        string sceneName = SceneManager.GetActiveScene().name;
        

        foreach (AudioSource source in _sources) {
            if (sceneName == "PlayScene") {
                source.volume = (float)OverNotesSystem.Instance.SettingItems[(int)SystemConstants.SettingItemTag.PlaySERate].GetValue();
                PlayerController.OnPressLane += PlaySE;
            }
        }
    }

    private void PlaySE(int index) {
        if(_sources[index] != null) {
            _sources[index].PlayOneShot(_sources[index].clip);
        }
    }

}

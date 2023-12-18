using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SEPlayer _sePlayer;
    [SerializeField] private double _offset;
    [SerializeField] static public bool SETask = false;

    private void Awake()
    {
        _audioSource.Stop();
    }

    private void Start()
    {
        OverNotesSystem system = OverNotesSystem.Instance;

        BeatmapData beatmapData = OverNotesSystem.Instance.GetBeatmap();
        _audioSource.clip = beatmapData.clip;
        _offset = beatmapData.offset + (system.Offset / 1000.0d);
        _audioSource.volume = (float)(system.SettingItems[(int)SystemConstants.SettingItemTag.BGMRate].GetValue()) / 100.0f;
    }

    private void FixedUpdate()
    {
        if (PlayContext.Routine >= PlayContext.PlayRoutine.Play)
        {
            PlayMusic();
        }

        if(OverNotesSystem.Instance.NowTime - _offset >= _audioSource.clip.length)
        {
            _audioSource.Stop();
            PlayContext.Routine = PlayContext.PlayRoutine.FadeIn;
        }
    }

    private void PlayMusic()
    {
        OverNotesSystem system = OverNotesSystem.Instance;

        system.NowTime += Time.deltaTime;
        double time1f = 1.0f / (float)system.SettingItems[(int)SystemConstants.SettingItemTag.NoteSpeed].GetValue();
        double addTime = time1f * 9.0f;
        PlayContext.DisplayTime = system.NowTime + addTime;

        if(system.NowTime > _offset &&
            !_audioSource.isPlaying &&
            PlayContext.Routine == PlayContext.PlayRoutine.Play)
        {
            float time = (float)(system.NowTime - _offset);
            _audioSource.time = time;
            _audioSource.Play();
        }
    }
}

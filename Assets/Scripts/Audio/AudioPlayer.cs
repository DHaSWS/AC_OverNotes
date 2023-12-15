using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] private double offset;
    [SerializeField] static public bool seTask = false;

    private void Awake()
    {
        audioSource.Stop();
    }

    private void Start()
    {
        BeatmapData beatmapData = OverNotesSystem.Instance.GetBeatmap();
        audioSource.clip = beatmapData.clip;
        offset = beatmapData.offset;
    }

    private void FixedUpdate()
    {
        if (PlayContext.Routine >= PlayContext.PlayRoutine.Play)
        {
            PlayMusic();
        }

        if(OverNotesSystem.Instance.NowTime - offset >= audioSource.clip.length)
        {
            audioSource.Stop();
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

        if(system.NowTime > offset &&
            !audioSource.isPlaying &&
            PlayContext.Routine == PlayContext.PlayRoutine.Play)
        {
            float time = (float)(system.NowTime - offset);
            audioSource.time = time;
            audioSource.Play();
        }
    }
}

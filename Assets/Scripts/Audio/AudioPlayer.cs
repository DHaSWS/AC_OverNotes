using OverNotes;
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
        BeatmapData beatmapData = SystemData.GetBeatmap();
        audioSource.clip = beatmapData.clip;
        offset = beatmapData.offset;
    }

    private void FixedUpdate()
    {
        if (PlayContext.routine >= PlayContext.Routine.Play)
        {
            PlayMusic();
        }

        if(SystemData.nowTime - offset >= audioSource.clip.length)
        {
            audioSource.Stop();
            PlayContext.routine = PlayContext.Routine.FadeIn;
        }
    }

    private void PlayMusic()
    {
        SystemData.nowTime = AudioSettings.dspTime - PlayContext.playDspTime;
        double time1f = 1.0f / SystemData.noteSpeed;
        double addTime = time1f * 9.0f;
        PlayContext.displayTime = SystemData.nowTime + addTime;

        if(SystemData.nowTime > offset &&
            !audioSource.isPlaying &&
            PlayContext.routine == PlayContext.Routine.Play)
        {
            float time = (float)(SystemData.nowTime - offset);
            audioSource.time = time;
            audioSource.Play();
        }
    }
}

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
        BeatmapData beatmapData = OverNotes.SystemData.GetBeatmap();
        audioSource.clip = beatmapData.clip;
        offset = beatmapData.offset;
    }

    private void FixedUpdate()
    {
        if (PlayContext.routine >= PlayContext.Routine.Play)
        {
            PlayMusic();
        }

        if(OverNotes.SystemData.nowTime - offset >= audioSource.clip.length)
        {
            audioSource.Stop();
            PlayContext.routine = PlayContext.Routine.FadeIn;
        }
    }

    private void PlayMusic()
    {
        OverNotes.SystemData.nowTime += Time.deltaTime;
        double time1f = 1.0f / OverNotes.SystemData.noteSpeed;
        double addTime = time1f * 9.0f;
        PlayContext.displayTime = OverNotes.SystemData.nowTime + addTime;

        if(OverNotes.SystemData.nowTime > offset &&
            !audioSource.isPlaying &&
            PlayContext.routine == PlayContext.Routine.Play)
        {
            float time = (float)(OverNotes.SystemData.nowTime - offset);
            audioSource.time = time;
            audioSource.Play();
        }
    }
}

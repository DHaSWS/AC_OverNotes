using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OverNotes;

public class TelopManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayContext.routine == PlayContext.Routine.Ready)
        {
            animator.SetInteger("Routine", 1);
        }
        else if(PlayContext.routine == PlayContext.Routine.Finish)
        {
            animator.SetInteger("Routine", 2);
        }
    }

    public void OnEndAnimationReady()
    {
        PlayContext.routine = PlayContext.Routine.Play;
        PlayContext.playDspTime = AudioSettings.dspTime;
    }

    public void OnEndAnimationFinish()
    {
        PlayContext.routine = PlayContext.Routine.FadeOut;
    }
}

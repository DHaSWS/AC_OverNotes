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
        if(PlayContext.Routine == PlayContext.PlayRoutine.Ready)
        {
            animator.SetInteger("Routine", 1);
        }
        else if(PlayContext.Routine == PlayContext.PlayRoutine.Finish)
        {
            animator.SetInteger("Routine", 2);
        }
    }

    public void OnEndAnimationReady()
    {
        PlayContext.Routine = PlayContext.PlayRoutine.Play;
        PlayContext.PlayDspTime = AudioSettings.dspTime;
    }

    public void OnEndAnimationFinish()
    {
        PlayContext.Routine = PlayContext.PlayRoutine.FadeOut;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeContext : MonoBehaviour
{
    static public float value = 0.0f;

    private enum State
    {
        Idle_FadeIn,
        FadeIn,
        Idle_FadeOut,
        FadeOut
    }

    static private State state = State.Idle_FadeOut;

    static public void SetFadeIn(
        MonoBehaviour monoBehaviour,
        float time,
        Image image,
        Action action
        )
    {
        if (state != State.Idle_FadeIn)
        {
            Debug.LogError("State is not Idle_FadeIn");
            return;
        }

        state = State.FadeIn;

        monoBehaviour.StartCoroutine(FadeIn(time, image, action));
    }

    static private IEnumerator FadeIn(
        float time,
        Image image,
        Action action
        )
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            value = i / time;
            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                value
                );
            yield return new WaitForSeconds(Time.deltaTime);
        }
        state = State.Idle_FadeOut;
        action();
    }

    static public void SetFadeOut(
        MonoBehaviour monoBehaviour,
        float time,
        Image image,
        Action action
        )
    {
        if (state != State.Idle_FadeOut)
        {
            Debug.LogError("State is not Idle_FadeOut");
            return;
        }

        state = State.FadeOut;

        monoBehaviour.StartCoroutine(FadeOut(time, image, action));
    }

    static private IEnumerator FadeOut(
        float time,
        Image image,
        Action action
        )
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            value = 1 - (i / time);
            image.color = new Color(
                image.color.r,
                image.color.g,
                image.color.b,
                value
                );
            yield return new WaitForSeconds(Time.deltaTime);
        }
        state = State.Idle_FadeIn;

        action();
    }
}


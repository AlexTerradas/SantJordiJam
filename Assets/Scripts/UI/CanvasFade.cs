using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CanvasFade : MonoBehaviour
{
    public float Duration = 0.4f;
    public float DelayFadeIn = 0f;
    public float DelayFadeOut = 0f;
    private CanvasGroup canvGroup;
    public bool StartWithFadeOut;

    public UnityEvent onShow;
    public UnityEvent onHide;
    
    private bool isFaded = true;
    private Coroutine currentFade;
    
    public void Awake()
    {
        canvGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        isFaded = canvGroup.alpha == 0;
        
        if (StartWithFadeOut)
        {
            canvGroup.alpha = 1f;
            canvGroup.interactable = false;
            canvGroup.blocksRaycasts = false;
            FadeOut();
        }
    }

    public void FadeIn()
    {
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }
        currentFade = StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1, DelayFadeIn));
        isFaded = false;
    }
    
    public void FadeIn(Action eventOnFinish)
    {
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }
        currentFade = StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1, DelayFadeIn, eventOnFinish));
        isFaded = false;
    }

    public void FadeOut()
    {
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }
        currentFade = StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0, DelayFadeOut));
        isFaded = true;
    }

    public void FadeOut(Action eventOnFinish)
    {
        if (currentFade != null)
        {
            StopCoroutine(currentFade);
        }
        currentFade = StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0, DelayFadeOut, eventOnFinish));
        isFaded = true;
    }
    
    public void Fade()
    {
        if (isFaded)
            FadeIn();
        else
            FadeOut();
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end, float delay)
    {
        float counter = 0f;

        if (end == 1)
        {
            canvGroup.interactable = true;
            canvGroup.blocksRaycasts = true;
        }
        else
        {
            canvGroup.interactable = false;
            canvGroup.blocksRaycasts = false;
        }

        yield return new WaitForSecondsRealtime(delay);

        while (counter < Duration)
        {
            counter += Time.unscaledDeltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }
    
    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end, float delay, Action onFinish)
    {
        float counter = 0f;

        if (end == 1)
        {
            canvGroup.interactable = true;
            canvGroup.blocksRaycasts = true;
        }
        else
        {
            canvGroup.interactable = false;
            canvGroup.blocksRaycasts = false;
        }

        yield return new WaitForSecondsRealtime(delay);

        while (counter < Duration)
        {
            counter += Time.unscaledDeltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }

        onFinish.Invoke();
    }
}

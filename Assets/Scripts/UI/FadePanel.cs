using System;
using System.Collections;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    public IEnumerator Fade (CanvasGroup canvas, float start, float end, float delay, float duration)
    {
        float counter = 0f;
        
        yield return new WaitForSecondsRealtime(delay);
        
        while (counter < duration)
        {
            counter += Time.unscaledDeltaTime;
            canvas.alpha = Mathf.Lerp(start, end, counter / duration);
            yield return null;
        }
    }
    
    public IEnumerator Fade (CanvasGroup canvas, float start, float end, float delay, float duration, Action changeScene)
    {
        float counter = 0f;
        
        yield return new WaitForSecondsRealtime(delay);
        
        while (counter < duration)
        {
            counter += Time.unscaledDeltaTime;
            canvas.alpha = Mathf.Lerp(start, end, counter / duration);
            yield return null;
        }

        changeScene();
    }
}

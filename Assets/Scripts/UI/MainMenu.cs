using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _currentCanvas;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeDelay = 0.5f;

    private void Start()
    {
        AudioManager.instance.PlayOneShot(AudioManager.instance.MainMenu);
    }

    public void StartGame()
    {
        StartCoroutine(Fade(_currentCanvas, _currentCanvas.alpha, 0, 0));
        //StopAudio
        SceneManager.LoadScene("Game");
    }

    public void ChangeMenu(CanvasGroup newCanvas)
    {
        _currentCanvas.interactable = false;
        _currentCanvas.blocksRaycasts = false;
        StartCoroutine(Fade(_currentCanvas, _currentCanvas.alpha, 0, 0));
        StartCoroutine(Fade(newCanvas, _currentCanvas.alpha, 1, fadeDelay));
        newCanvas.interactable = true;
        newCanvas.blocksRaycasts = true;
        _currentCanvas = newCanvas;
    }
    
    public IEnumerator Fade (CanvasGroup canvas, float start, float end, float delay)
    {
        float counter = 0f;
        
        yield return new WaitForSecondsRealtime(delay);
        
        while (counter < fadeDuration)
        {
            counter += Time.unscaledDeltaTime;
            canvas.alpha = Mathf.Lerp(start, end, counter / fadeDuration);
            yield return null;
        }
    }

    public void AnimateCamera(string trigger)
    {
        _cameraAnimator.SetTrigger(trigger);
    }

    public void ButtonClick()
    {
        AudioManager.instance.PlayOneShot(AudioManager.instance.ButtonClick);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
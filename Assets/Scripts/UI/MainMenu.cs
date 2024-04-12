using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _currentCanvas;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeDelay = 0.5f;
    public void StartGame()
    {
        StartCoroutine(Fade(_currentCanvas, _currentCanvas.alpha, 0));
        SceneManager.LoadScene("Game");
    }

    public void ChangeMenu(CanvasGroup newCanvas)
    {
        _currentCanvas.interactable = false;
        _currentCanvas.blocksRaycasts = false;
        StartCoroutine(Fade(_currentCanvas, _currentCanvas.alpha, 0));
        StartCoroutine(Fade(newCanvas, _currentCanvas.alpha, 1));
        newCanvas.interactable = true;
        newCanvas.blocksRaycasts = true;
        _currentCanvas = newCanvas;
    }
    
    public IEnumerator Fade (CanvasGroup canvas, float start, float end)
    {
        float counter = 0f;
        
        yield return new WaitForSecondsRealtime(fadeDelay);
        
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
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
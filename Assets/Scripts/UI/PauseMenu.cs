using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private InputAction pauseAction;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private CanvasGroup currentCanvas;
    [SerializeField] private float fadeDelay = 0.5f;
    
    private bool _paused;

    private void Start()
    {
        //Finished = false;
        _paused = false;
    }

    void Update()
    {

    }
    
    public void PauseGame()
    {
        print("Esc has been pressed");
        if (_paused)
        {
            Resume();
            print("Resume Game");
        }
        else
        {
            Pause();
            print("Pause Game");
        }
    }

    public void ChangeMenu(CanvasGroup newCanvas)
    {
        currentCanvas.interactable = false;
        currentCanvas.blocksRaycasts = false;
        StartCoroutine(Fade(currentCanvas, currentCanvas.alpha, 0));
        StartCoroutine(Fade(newCanvas, currentCanvas.alpha, 1));
        newCanvas.interactable = true;
        newCanvas.blocksRaycasts = true;
        currentCanvas = newCanvas;
    }

    void Pause()
    {
        Time.timeScale = 0.0f;
        StartCoroutine(Fade(currentCanvas, currentCanvas.alpha, 1));
        _paused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(Fade(currentCanvas, currentCanvas.alpha, 0));
        _paused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    // public void RestartGame()
    // {
    //     pauseMenuUI.SetActive(false);
    //     _paused = false;
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

    // public void LoadScene(string scene_name)
    // {
    //     LoadingScreen.SetActive(true);
    //     var Loading = LoadingScreen.GetComponent<Animator>();
    //     Loading.SetTrigger("Move");
    //     StartCoroutine(LoadAsynchronously(scene_name));
    // }

    // IEnumerator LoadAsynchronously(string scene_name)
    // {
    //     Fade();

    //     yield return new WaitForSeconds(1.5f);

    //     SceneManager.LoadScene(scene_name);

    //     yield return new WaitForSeconds(2f);
    // }



    // public void Fade()
    // {
    //     StartCoroutine(Fade(canvGroup, canvGroup.alpha, Faded ? 1 : 0));
    //     Faded = !Faded;
    // }
    
    //Cosita de canvi de menu
    public IEnumerator Fade (CanvasGroup canvas, float start, float end)
    {
        float counter = 0f;
        
        while (counter < fadeDuration)
        {
            counter += Time.unscaledDeltaTime;
            canvas.alpha = Mathf.Lerp(start, end, counter / fadeDuration);
            yield return null;
        }
    }
}
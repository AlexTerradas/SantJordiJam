using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    
    public bool Faded;
    public bool Finished;
    public GameObject pauseMenuUI;
    public GameObject LoadingScreen;
    public CanvasGroup canvGroup;
    
    private bool _paused;

    private void Start()
    {
        Finished = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    private void OnPauseKey()
    {
        print("Escape is pressed");
    }

    public void RestartGame()
    {
        pauseMenuUI.SetActive(false);
        _paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        Fade();
        _paused = false;
    }

    public void LoadScene(string scene_name)
    {
        LoadingScreen.SetActive(true);
        var Loading = LoadingScreen.GetComponent<Animator>();
        Loading.SetTrigger("Move");
        StartCoroutine(LoadAsynchronously(scene_name));
    }

    IEnumerator LoadAsynchronously(string scene_name)
    {
        Fade();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(scene_name);

        yield return new WaitForSeconds(2f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void PauseGame()
    {
        Fade();
        _paused = true;
    }

    public void Fade()
    {
        StartCoroutine(Fade(canvGroup, canvGroup.alpha, Faded ? 1 : 0));
        Faded = !Faded;
    }
    
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
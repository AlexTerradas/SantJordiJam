using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    private float Duration = 1f;

    public bool Faded;
    public bool Finished;
    public GameObject pauseMenuUI;
    public GameObject LoadingScreen;
    public CanvasGroup canvGroup;
    public static bool gameIsPaused = false;

    private void Start()
    {
        Finished = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void RestartGame()
    {
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        Fade();
        gameIsPaused = false;
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
        gameIsPaused = true;
    }

    public void Fade()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, Faded ? 1 : 0));

        Faded = !Faded;
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
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

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }

        Finished = true;
    }
}
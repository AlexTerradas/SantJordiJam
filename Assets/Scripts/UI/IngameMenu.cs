using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    public CanvasFade BlackScreen;
    protected CanvasGroup canvasGroup;

    protected void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void ShowMenu()
    {
        GetComponent<CanvasFade>().FadeIn();
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        BlackScreen.FadeIn(() => SceneManager.LoadScene("MainMenu"));
    }

    public void QuitGame()
    {
        BlackScreen.FadeIn(() => Application.Quit());
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasFade LoadingScreen;
    private CanvasGroup _canvas;
    public CanvasFade MainMenuCanvas;
    public CanvasFade OptionsCanvas;
    public CanvasFade ControlsCanvas;
    public CanvasFade CreditsCanvas;

    private CanvasFade currentShownCanvas;
    
    private void Start()
    {
        Time.timeScale = 1;

        _canvas = MainMenuCanvas.GetComponent<CanvasGroup>();
        LoadingScreen.gameObject.SetActive(true);
        LoadingScreen.FadeOut();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        currentShownCanvas = MainMenuCanvas;
    }

    public void StartGame()
    {
        _canvas.interactable = false;
        LoadingScreen.gameObject.SetActive(true);
        LoadingScreen.FadeIn(() => SceneManager.LoadScene("Core"));
    }

    public void MainMenuFade()
    {
        ChangeMenu(MainMenuCanvas);
    }

    public void ControlsMenu()
    {
        ChangeMenu(ControlsCanvas);
    }

    public void OptionsMenu()
    {
        ChangeMenu(OptionsCanvas);
    }

    public void CreditsMenu()
    {
        ChangeMenu(CreditsCanvas);
    }

    private void ChangeMenu(CanvasFade newCanvas)
    {
        currentShownCanvas.FadeOut();
        currentShownCanvas = newCanvas;
        currentShownCanvas.FadeIn();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

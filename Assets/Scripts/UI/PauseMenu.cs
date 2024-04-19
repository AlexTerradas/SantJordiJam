using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private InputAction pauseAction;
    [SerializeField] private CanvasGroup currentCanvas;
    [SerializeField] private CanvasGroup backgroundCanvas;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeDelay;
    private EventInstance inGameSong;
    private FadePanel _fade;
    private bool _paused;

    private void Start()
    {
        inGameSong = AudioManager.instance.CreateEventInstance(AudioManager.instance.Music);
        AudioManager.instance.PlaySong(inGameSong);
        _fade = GetComponent<FadePanel>();
        _paused = false;
        
        Resume();
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
        _fade.StartCoroutine(_fade.Fade(currentCanvas, currentCanvas.alpha, 0, 0, fadeDuration));
        _fade.StartCoroutine(_fade.Fade(newCanvas, currentCanvas.alpha, 1, fadeDelay, fadeDuration));
        newCanvas.interactable = true;
        newCanvas.blocksRaycasts = true;
        currentCanvas = newCanvas;
    }

    void Pause()
    {
        Time.timeScale = 0.0f;
        _fade.StartCoroutine(_fade.Fade(backgroundCanvas, 0, 1, 0, 0.01f));
        _fade.StartCoroutine(_fade.Fade(currentCanvas, 0, 1, 0, 0.01f));
        AudioManager.instance.PauseSong(inGameSong);
        _paused = true;
    }
    
    public void Resume()
    {
        Time.timeScale = 1.0f;
        _fade.StartCoroutine(_fade.Fade(backgroundCanvas, 1, 0, 0, 0.01f));
        _fade.StartCoroutine(_fade.Fade(currentCanvas, 1, 0, 0, 0.01f));
        AudioManager.instance.ResumeSong(inGameSong);
        _paused = false;
    }
    
    public void ButtonClick()
    {
        AudioManager.instance.PlayOneShot(AudioManager.instance.ButtonClick);
    }

    public void StopInGameSong()
    {
        AudioManager.instance.StopSong(inGameSong);
    }
    
    //public void QuitGame()
    //{
    //    Application.Quit();
    //}
    
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
}
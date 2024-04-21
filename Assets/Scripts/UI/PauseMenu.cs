﻿using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private InputAction pauseAction;

    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup pauseMenuCanvas;
    [SerializeField] private CanvasGroup optionsMenuCanvas;
    [SerializeField] private CanvasGroup currentCanvas;
    [SerializeField] private CanvasGroup backgroundCanvas;

    [Header("Properties")]
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeDelay;
    private FadePanel _fade;
    private bool _paused;

    private void Start()
    {
        _fade = GetComponent<FadePanel>();
        _fade.StartCoroutine(_fade.Fade(backgroundCanvas, 1, 0, 0, 0.01f));
        _fade.StartCoroutine(_fade.Fade(currentCanvas, 1, 0, 0, 0.01f));
        _paused = false;
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
        _fade.StartCoroutine(_fade.Fade(backgroundCanvas, 0, 1, 0, 0.01f));
        _fade.StartCoroutine(_fade.Fade(currentCanvas, 0, 1, 0, 0.01f));

        AudioManager.instance.PlayOneShot(AudioManager.instance.PauseOn);
        AudioManager.instance.PauseSong(AudioManager.instance.inGameSong);

        currentCanvas.interactable = true;
        currentCanvas.blocksRaycasts = true;

        _paused = true;
        Time.timeScale = 0.0f;
    }
    
    public void Resume()
    {
        _fade.StartCoroutine(_fade.Fade(backgroundCanvas, 1, 0, 0, 0.01f));
        _fade.StartCoroutine(_fade.Fade(currentCanvas, 1, 0, 0, 0.01f));

        currentCanvas.interactable = false;
        currentCanvas.blocksRaycasts = false;

        AudioManager.instance.PlayOneShot(AudioManager.instance.PauseOff);
        AudioManager.instance.ResumeSong(AudioManager.instance.inGameSong);

        currentCanvas = pauseMenuCanvas;
        StartCoroutine(ResumeCheck());

        _paused = false;
        Time.timeScale = 1.0f;
    }
    
    public void ButtonClick()
    {
        AudioManager.instance.PlayOneShot(AudioManager.instance.ButtonClick);
    }

    public void StopInGameSong()
    {
        AudioManager.instance.StopSong(AudioManager.instance.inGameSong);
    }

    IEnumerator ResumeCheck()
    {
        yield return new WaitForSeconds(0.05f);
        
        if(pauseMenuCanvas.alpha > 0)
            pauseMenuCanvas.alpha = 0;

        if(optionsMenuCanvas.alpha > 0)
            optionsMenuCanvas.alpha = 0;
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
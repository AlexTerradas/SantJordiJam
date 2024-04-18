﻿using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _currentCanvas;
    [SerializeField] private CanvasGroup _blackCanvas;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _fadeDelay;
    private EventInstance mainMenuSong;
    private FadePanel _fade;
    
    private void Start()
    {
        _fade = GetComponent<FadePanel>();
        _fade.StartCoroutine(_fade.Fade(_blackCanvas, 1, 0, 0, _fadeDuration));
        mainMenuSong = AudioManager.instance.CreateEventInstance(AudioManager.instance.MainMenu);
        AudioManager.instance.PlaySong(mainMenuSong);
    }

    public void StartGame()
    {
        _fade.StartCoroutine(_fade.Fade(_blackCanvas, 0, 1, 0, _fadeDuration, ChangeScene));
    }
    
    public void ChangeMenu(CanvasGroup newCanvas)
    {
        _currentCanvas.interactable = false;
        _currentCanvas.blocksRaycasts = false;
        _fade.StartCoroutine(_fade.Fade(_currentCanvas, _currentCanvas.alpha, 0, 0, _fadeDuration));
        _fade.StartCoroutine(_fade.Fade(newCanvas, _currentCanvas.alpha, 1, _fadeDelay, _fadeDuration));
        newCanvas.interactable = true;
        newCanvas.blocksRaycasts = true;
        _currentCanvas = newCanvas;
    }

    public void ChangeScene()
    {
        AudioManager.instance.StopSong(mainMenuSong);
        SceneManager.LoadScene("CosasDaro");
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
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent displayLocalizationEvent;
    [SerializeField] private string _windowedKey;
    [SerializeField] private string _fullscreenKey;
    [SerializeField] private TMP_Text _resolutionText;
    
    private Resolution[] _resolutions;
    private string[] _resolutionNames;
    private int _currentResolution;

    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    
    [Range(0, 10)]
    public float _sfxVolume = 10;
    [Range(0, 10)]
    public float _musicVolume = 10;

    private Bus _sfxBus;
    private Bus _musicBus;
    
    private void Start()
    {
        _resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string resolution = _resolutions[i].width + "x" + _resolutions[i].height;
            if (options.Contains(resolution.ToLower())) continue;
            options.Add(resolution);
        }
        
        _resolutionNames = options.ToArray();

        _sfxBus = RuntimeManager.GetBus("bus:/SFX");
        _musicBus = RuntimeManager.GetBus("bus:/Music");

        // Set sliders volume start
        _sfxBus.getVolume(out _sfxVolume);
        _musicBus.getVolume(out _musicVolume);

        _sfxSlider.value = _sfxVolume;
        _musicSlider.value = _musicVolume;

        _resolutionText.text = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
    }

    private void Update()
    {
        AudioManager.instance.sfxVolume = _sfxVolume;
        AudioManager.instance.musicVolume = _musicVolume;
        _sfxBus.setVolume(AudioManager.instance.sfxVolume/10);
        _musicBus.setVolume(AudioManager.instance.musicVolume/10);
    }

    public void IncreaseSFXVolume()
    {
        _sfxVolume++;
        _sfxSlider.value = _sfxVolume;
    }

    public void DecreaseSFXVolume()
    {
        _sfxVolume--; 
        _sfxSlider.value = _sfxVolume;
    }
    public void SetSFXVolume(float value) { _sfxVolume = value; }

    public void IncreaseMusicVolume()
    {
        _musicVolume++; 
        _musicSlider.value = _musicVolume;
    }

    public void DecreaseMusicVolume()
    {
        _musicVolume--; 
        _musicSlider.value = _musicVolume;
    }
    public void SetMusicVolume(float value) { _musicVolume = value; }

    public void SwapDisplayMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
        displayLocalizationEvent.SetEntry(Screen.fullScreen ? _windowedKey : _fullscreenKey);
    }
    
    public void IncreaseResolution()
    {
        if (_currentResolution < _resolutionNames.Length)
            _currentResolution++;
        else
            _currentResolution = 1;
        
        SetResolution();
    }

    public void DecreaseResolution()
    {
        if (_currentResolution > 1)
            _currentResolution--;
        else
            _currentResolution = _resolutionNames.Length;
        
        SetResolution();
    }
    
    public void SetResolution()
    {
        int width = int.Parse(_resolutionNames[_currentResolution - 1].Split('x')[0]);
        int height = int.Parse(_resolutionNames[_currentResolution - 1].Split('x')[1]);
        _resolutionText.text = _resolutionNames[_currentResolution - 1];
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}
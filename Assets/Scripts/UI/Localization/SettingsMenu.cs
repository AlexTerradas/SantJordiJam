using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private TMP_Text _resolutionText;
    
    private Resolution[] _resolutions;
    private string[] _resolutionNames;
    private int _currentResolution; 
    
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
    }

    public void SwapDisplayMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
        _displayText.text = Screen.fullScreen ? "Fullscreen" : "Windowed";
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
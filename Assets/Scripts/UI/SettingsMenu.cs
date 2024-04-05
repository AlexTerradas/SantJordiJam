using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private CanvasFade soundCanvas;
    [SerializeField] private CanvasFade videoCanvas;
    private CanvasFade currentShownCanvas;
    
    [SerializeField] private HighlightText soundCanvasButton;
    [SerializeField] private HighlightText videoCanvasButton;

    private bool setAppToFullscreen = true;
    [SerializeField] private TextMeshProUGUI displayText;
    
    Resolution[] resolutions;
    string[] resolutionsNames;
    int actualResolution; 
    [SerializeField] private TextMeshProUGUI resolutionText;

    private void Start()
    {
        setAppToFullscreen = PlayerPrefs.GetInt("Fullscreen") == 0; // 0 == Fullscreen, 1 == Windowed
        Screen.fullScreen = setAppToFullscreen;
        displayText.text = setAppToFullscreen ? "Fullscreen" : "Windowed";

        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        int iter = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            if (options.Contains(option.ToLower()))
            {
                continue;
            }

            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = iter;
            }

            iter++;
        }

        resolutionsNames = options.ToArray();
        
        if (PlayerPrefs.GetInt("Resolution") == 0)
        {
            actualResolution = resolutionsNames.Length;
        }
        else
        {
            actualResolution = PlayerPrefs.GetInt("Resolution");
        }
        
        SetResolutionText();

        currentShownCanvas = soundCanvas;
        soundCanvasButton.SetSelectedState(true);
        videoCanvasButton.SetSelectedState(false);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeWindowedMode()
    {
        setAppToFullscreen = !setAppToFullscreen;
        Screen.fullScreen = setAppToFullscreen;
        displayText.text = setAppToFullscreen ? "Fullscreen" : "Windowed";
    }

    public void SetResolution()
    {
        var width = int.Parse(resolutionsNames[actualResolution - 1].Split('x')[0]);
        var height = int.Parse(resolutionsNames[actualResolution - 1].Split('x')[1]);

        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void SetSoundWindow()
    {
        if (currentShownCanvas != soundCanvas)
        {
            ChangeMenu(soundCanvas);
            soundCanvasButton.SetSelectedState(true);
            videoCanvasButton.SetSelectedState(false);
        }
    }

    public void SetVideoWindow()
    {
        if (currentShownCanvas != videoCanvas)
        {
            ChangeMenu(videoCanvas);
            soundCanvasButton.SetSelectedState(false);
            videoCanvasButton.SetSelectedState(true);
        }
    }

    private void ChangeMenu(CanvasFade newCanvas)
    {
        currentShownCanvas.FadeOut();
        currentShownCanvas = newCanvas;
        currentShownCanvas.FadeIn();
    }
    
    public void SetResolutionText()
    {        
        resolutionText.text = resolutionsNames[actualResolution - 1];
    }

    public void IncreaseResolution()
    {
        if (actualResolution < resolutionsNames.Length)
            actualResolution++;
        else
            actualResolution = 1;


        SetResolutionText();
        SetResolution();
    }

    public void DecreaseResolution()
    {
        if (actualResolution > 1)
            actualResolution--;
        else
            actualResolution = resolutionsNames.Length;

        SetResolutionText();
        SetResolution();
    }
}
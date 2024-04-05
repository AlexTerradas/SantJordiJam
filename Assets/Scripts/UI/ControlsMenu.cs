using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private CanvasFade keyboardCanvas;
    [SerializeField] private CanvasFade controllerCanvas;
    private CanvasFade currentShownCanvas;
    
    [SerializeField] private HighlightText keyboardCanvasButton;
    [SerializeField] private HighlightText controllerCanvasButton;

    private void Start()
    {
        currentShownCanvas = keyboardCanvas;
        keyboardCanvasButton.SetSelectedState(true);
        controllerCanvasButton.SetSelectedState(false);
    }

    public void SetKeyboardWindow()
    {
        if (currentShownCanvas != keyboardCanvas)
        {
            ChangeMenu(keyboardCanvas);
            keyboardCanvasButton.SetSelectedState(true);
            controllerCanvasButton.SetSelectedState(false);
        }
    }

    public void SetControllerWindow()
    {
        if (currentShownCanvas != controllerCanvas)
        {
            ChangeMenu(controllerCanvas);
            keyboardCanvasButton.SetSelectedState(false);
            controllerCanvasButton.SetSelectedState(true);
        }
    }

    private void ChangeMenu(CanvasFade newCanvas)
    {
        currentShownCanvas.FadeOut();
        currentShownCanvas = newCanvas;
        currentShownCanvas.FadeIn();
    }
}
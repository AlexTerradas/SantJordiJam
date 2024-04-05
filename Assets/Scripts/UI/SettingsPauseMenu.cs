using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsPauseMenu : IngameMenu
{
    [HideInInspector] public bool IsPaused = false;
    private PlayerInput playerInput;

    private DepthOfField depthOfField;
    [SerializeField] private VolumeProfile volumeProfile;

    protected void Awake()
    {
        base.Awake();

        playerInput = FindObjectOfType<PlayerInput>();
        
    }

    private void OnEnable()
    {
        DepthOfField tmp;
        
        if (volumeProfile.TryGet<DepthOfField>(out tmp))
        {
            depthOfField = tmp;
        }

        depthOfField.active = false;
    }

    private void OnDisable()
    {
        depthOfField.active = false;
    }

    public void PauseGame()
    {
        playerInput.actions.FindActionMap("Player").Disable();
        playerInput.actions.FindActionMap("UI").Enable();
        Time.timeScale = 0;

        IsPaused = true;
        SetState(IsPaused);
    }

    public void Continue()
    {
        playerInput.actions.FindActionMap("Player").Enable();
        playerInput.actions.FindActionMap("UI").Disable();
        Time.timeScale = 1;

        IsPaused = false;
        SetState(false);
    }

    public void SetState(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
        
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
        canvasGroup.alpha = value ? 1f : 0f;

        depthOfField.active = value;
    }

    public void RestartOnCheckpoint()
    {
        //CharacterSafeAreaCheckpoint.Instance.RespawnOnCheckpoint();
    }
    
    public void RestartScene()
    {
        BlackScreen.FadeIn(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }
}

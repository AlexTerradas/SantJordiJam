using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameOverMenu : IngameMenu
{
    /*private DepthOfField depthOfField;
    [SerializeField] private VolumeProfile volumeProfile;
    
    public void RestartScene()
    {
        BlackScreen.FadeIn(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
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

    public override void ShowMenu()
    {
        base.ShowMenu();

        var playerInput = FindObjectOfType<PlayerInput>();
        Destroy(playerInput.GetComponent<PauseMenuInputs>());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        playerInput.actions.FindActionMap("Player").Disable();
        playerInput.actions.FindActionMap("UI").Enable();
    }*/
}
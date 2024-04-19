using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputControls : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private InputAction leftButton;
    [SerializeField]
    private InputAction rightButton;
    [SerializeField]
    private InputAction spaceAction;
    [SerializeField]
    private InputAction pauseAction;

    [Header("UI")]
    [SerializeField] 
    private PauseMenu pauseMenu;

    [Header("Player")]
    public PlayerController playerController;

    private void OnMovement(InputValue input)
    {
        Vector2 movementInput = input.Get<Vector2>();
        print("X: " + movementInput.x);
        playerController.MovePointXAxis(movementInput.x);
    }
    private void OnSpaceKey()
    {
        print("Space Key is pressed");
        playerController.InteractWithPoint();
    }
    private void OnPauseKey()
    {
        //print("Escape is pressed");
        pauseMenu.PauseGame();
    }
}

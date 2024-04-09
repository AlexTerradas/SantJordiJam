using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputControlsDummy : MonoBehaviour
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
    private InputAction anyKeyAction;
    [SerializeField]
    private InputAction pauseAction;
    
    void Awake() 
    {
        
    }
    void Update()
    {

    }

    private void OnMovement(InputValue input)
    {
        Vector2 movementInput = input.Get<Vector2>();
        print("X: " + movementInput.x);
    }

    private void OnSpaceKey()
    {
        print("Space Key is pressed");
    }

    private void OnGKey()
    {
        print("G Key is pressed");
    }
    private void OnPauseKey()
    {
        print("Escape is pressed");
    }
}

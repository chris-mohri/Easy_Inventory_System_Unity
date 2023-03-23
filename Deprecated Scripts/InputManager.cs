using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    PlayerControls controls;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    //private bool  jump=false;


    private void Awake()
    {
        //OnEnable();
        //controls.Player.Jump.performed+=cntxt=> jump=true;
        //controls.Player.Jump.canceled+=cntxt=> jump=false;
    }

    private void OnEnable()
    {
        if (controls==null)
        {
            controls = new PlayerControls();
            controls.Player.Movement.performed+=i=>movementInput = i.ReadValue<Vector2>();
        }

        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    public bool jumpTrigger()
    {
        //Debug.Log(controls.Player.Jump.phase);
        //controls.Player.Jump.performed +=cntxt => Debug.Log(controls.Player.Jump.phase);
        return false;
        
        //return jump;
        //return controls.Player.Jump.triggered;
    }

    private void Update()
    {
        //jump=false;
    }

    
    



    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
}
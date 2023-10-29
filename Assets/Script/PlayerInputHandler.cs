using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movementInput;
    public bool LeftFired;
    public bool RightFired;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLeftFireInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            LeftFired = true;
        }
        else if (context.canceled)
        {
            LeftFired = false;
        }
    }

    public void OnRightFireInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RightFired = true;
        }
        else if (context.canceled)
        {
            RightFired = false;
        }
    }
}

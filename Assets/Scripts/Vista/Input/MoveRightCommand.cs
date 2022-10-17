using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightCommand : InputCommand
{
    public bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.D);
    }

    public void Execute(InputManager im)
    {
        im.MoveRight();
    }

    
}

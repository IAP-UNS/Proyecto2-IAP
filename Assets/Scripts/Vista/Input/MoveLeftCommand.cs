using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCommand : InputCommand
{
    public bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    public void Execute(InputManager im)
    {
        im.MoveLeft();
    }


}

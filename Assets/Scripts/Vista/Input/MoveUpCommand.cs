using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpCommand : InputCommand
{
    public bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    public void Execute(InputManager im)
    {
        im.MoveUp();
    }


}

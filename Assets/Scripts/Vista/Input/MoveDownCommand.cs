using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownCommand : InputCommand
{
    public bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.S);
    }

    public void Execute(InputManager im)
    {
        im.MoveDown();
    }


}

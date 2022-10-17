using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceSpeedCommand : InputCommand
{
    public bool CheckCondition()
    {
        return Input.GetKeyUp(KeyCode.LeftShift);
    }

    public void Execute(InputManager im)
    {
        im.ReduceSpeed();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedCommand : InputCommand
{
    public bool CheckCondition()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public void Execute(InputManager im)
    {
        im.IncreaseSpeed();
    }


}

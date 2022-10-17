using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputCommand 
{
    public bool CheckCondition();
    public void Execute(InputManager im);
}

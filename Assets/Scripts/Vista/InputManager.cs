using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public float speed = 0.2f;

    private LogicSnake logicSnake;

    public void SetLogicSnake(LogicSnake ls)
    {
        logicSnake = ls;
    }

    public void UpdatePlayer()
    {
        //esto quedaría super lindo
        //hacer un patrón state y tal vez un command
        if (Input.GetKeyDown(KeyCode.D))
        {
            logicSnake.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            logicSnake.MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            logicSnake.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            logicSnake.MoveDown();
        }
        //mientras se presiona el shift izquierdo, se incrementa la velocidad
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 0.05f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 0.2f;
        }
    }



    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}

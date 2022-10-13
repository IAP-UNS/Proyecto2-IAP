using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private int currentDirection = 0;
    public float speed = 0.2f;

    public void UpdatePlayer()
    {
        //esto quedaría super lindo
        //hacer un patrón state y tal vez un command
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentDirection != 3) currentDirection = 1;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentDirection != 4) currentDirection = 2;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentDirection != 1) currentDirection = 3;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentDirection != 2) currentDirection = 4;
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

    public int GetCurrentDirection()
    {
        return currentDirection;
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

using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public float speed = 0.2f;

    private LogicSnake logicSnake;
    private List<InputCommand> commands;

    private void Start()
    {
        commands = new List<InputCommand>();
        commands.Add(new MoveRightCommand());
        commands.Add(new MoveUpCommand());
        commands.Add(new MoveDownCommand());
        commands.Add(new MoveLeftCommand());
        commands.Add(new IncreaseSpeedCommand());
        commands.Add(new ReduceSpeedCommand());
    }

    private void Update()
    {
        UpdatePlayer();
    }

    public void SetLogicSnake(LogicSnake ls)
    {
        logicSnake = ls;
    }

    public void UpdatePlayer()
    {
        foreach(InputCommand ic in commands)
        {
            if (ic.CheckCondition())
            {
                ic.Execute(this);
            }
        }
    }

    public void MoveRight()
    {
        logicSnake.MoveRight();
    }

    public void MoveUp()
    {
        logicSnake.MoveUp();
    }

    public void MoveLeft()
    {
        logicSnake.MoveLeft();
    }

    public void MoveDown()
    {
        logicSnake.MoveDown();
    }

    public void IncreaseSpeed()
    {
        speed = 0.05f;
    }

    public void ReduceSpeed()
    {
        speed = 0.2f;
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

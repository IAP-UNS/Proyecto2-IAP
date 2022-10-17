using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEntity : Entity
{
    public void Collision(GameManager gm)
    {
        gm.GameOver();
    }
}

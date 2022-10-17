using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEntity : Entity
{
    public void Collision(GameManager gm)
    {
        gm.GameOver();
    }
}

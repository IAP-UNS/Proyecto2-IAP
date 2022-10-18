using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitEntity : Entity
{
    public void Collision(GameManager gm)
    {
        gm.FruitGrabbed();
    }
}

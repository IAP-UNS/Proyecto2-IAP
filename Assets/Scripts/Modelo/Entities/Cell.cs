using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    private List<Entity> entities;

    public Cell()
    {
        entities = new List<Entity>();
    }

    public void Collision(GameManager gm)
    {
        if (!IsEmpty())
        {
            entities[0].Collision(gm);
        }
    }


    public bool IsEmpty()
    {
        return entities.Count == 0;
    }


    public void AddEntity(Entity e)
    {
        entities.Add(e);
    }

    public void EmptyCell()
    {
        entities = new List<Entity>();
    }


    /*
    private Entity snake, wall, fruit;
    public Entity Snake { get => snake; set => snake = value; }
    public Entity Wall { get => wall; set => wall = value; }
    public Entity Fruit { get => fruit; set => fruit = value; }

    public bool HasWall()
    {
        return wall != null;
    }
    public bool HasSnake()
    {
        return snake != null;
    }
    public bool HasFruit()
    {
        return fruit != null;
    }
    */


}


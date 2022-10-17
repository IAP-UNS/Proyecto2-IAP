using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map 
{
    private Cell[,] matrix;

    public Map()
    {
        matrix = new Cell[40, 40];
        for (int i = 0; i < 40; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                matrix[i, j] = new Cell();
            }
        }
    }

    public Vector3Int GetFreePosition()
    {
        bool isAvailable = false;
        Vector3Int newFreePosition = new Vector3Int(0, 0, 0);
        while (!isAvailable)
        {
            newFreePosition = new Vector3Int(Random.Range(1, 40), 0, Random.Range(1, 40));
            isAvailable = IsFreeSurroundings(newFreePosition);
        }
        return newFreePosition;
    }

    public bool IsFreeSurroundings(Vector3Int pos)
    {
        bool isAvailable = true;
        for (int i = pos.x - 1; i < pos.x + 1 && isAvailable; i++)
        {
            for (int j = pos.z - 1; j < pos.z + 1 && isAvailable; j++)
            {
                isAvailable = matrix[i,j].IsEmpty();
            }//ver cambiar iswallat por isAnythigAt
        }
        return isAvailable;
    }

    /*
    public bool IsWallAt(int r, int c)
    {
        return matrix[r, c].HasWall();
    }

    public bool IsFruitAt(int r, int c)
    {
        return matrix[r, c].HasFruit();
    }

    public bool IsSnakeAt(int r, int c)
    {
        return matrix[r, c].HasSnake();
    }

    public void PlaceSnakePartAt(int r, int c)
    {
        matrix[r, c].Snake = (new SnakeEntity());
    }
    public void FreeSnakePartAt(int r, int c)
    {
        matrix[r, c].Snake = null;
    }

    public void PlaceWallAt(int r, int c)
    {
        matrix[r, c].Wall = (new WallEntity());
    }

    public void PlaceFrutitaAt(int r, int c)
    {
        matrix[r, c].Fruit = (new FruitEntity());
    }

    public void FreeFruitAt(int r, int c)
    {
        matrix[r, c].Fruit = null;
    }
    */

    public void AddEntityAt(Entity e, int x, int z)
    {
        matrix[x, z].AddEntity(e);
    }

    public void EmptyCellAt(int x, int z)
    {
        matrix[x, z].EmptyCell();
    }


    public int RandomWallValue()
    {
        return Random.Range(50, 50);
    }

    public Cell GetCellAt(int x, int z)
    {
        return matrix[x, z];
    }


    public List<Vector3Int> GetWallsPositions()
    {
        List<Vector3Int> walls = new List<Vector3Int>();
        for(int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (!matrix[i, j].IsEmpty())
                {
                    walls.Add(new Vector3Int(i, 0, j));
                }
            }
        }
        return walls;
    }
}

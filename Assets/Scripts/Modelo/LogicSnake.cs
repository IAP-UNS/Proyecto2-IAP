using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modelo
{

    public class LogicSnake
    {
        private List<Vector3Int> snakePositions;
        private Vector3Int ultimaPosicion;


        GameManager gameManager;

        public void InitSnake(GameManager gm)
        {
            gameManager = gm;
            snakePositions = new List<Vector3Int>();
        }

        public Vector3Int UpdateSnake()
        {
            ultimaPosicion = GetTail();
            FreeSnakeTailPosition();
            UpdateSnakePositionsWhileMoving();
            Vector3Int pos = GetHead();

            if (gameManager.GetCurrentDirection() == 1)
            {
                pos.x += 1;
            }
            else if (gameManager.GetCurrentDirection() == 2)
            {
                pos.z += 1;
            }
            else if (gameManager.GetCurrentDirection() == 3)
            {
                pos.x -= 1;
            }
            else if (gameManager.GetCurrentDirection() == 4)
            {
                pos.z -= 1;
            }

            return pos;
        }

        private void FreeSnakeTailPosition()
        {
            gameManager.FreeSnakePartAt(GetTail().x, GetTail().z);
        }



        public void UpdateSnakeHeadPosition(Vector3Int nuevaPos)
        {
            UpdateHeadPosition(nuevaPos);
            gameManager.PlaceSnakePartAt(GetHead().x, GetHead().z);
        }


        public Vector3Int GetHead()
        {
            return snakePositions[snakePositions.Count - 1];
        }

        public Vector3Int GetTail()
        {
            return snakePositions[0];
        }

        public int GetLength()
        {
            return snakePositions.Count;
        }

        public void UpdateSnakePositionsWhileMoving()
        {
            for (int i = 0; i < snakePositions.Count - 1; i++)
            {
                snakePositions[i] = snakePositions[i + 1];
            }
        }

        public void UpdateHeadPosition(Vector3Int newPosition)
        {
            snakePositions[snakePositions.Count - 1] = newPosition;
        }

        public void UpdateTailPosition(Vector3Int newPosition)
        {
            snakePositions.Insert(0, newPosition);
        }

        public Vector3Int GetSnakePartPosition(int i)
        {
            return snakePositions[i];
        }

        public void AddNewPart(Vector3Int nuevaPos)
        {
            snakePositions.Add(nuevaPos);
        }

        public Vector3Int GetUltimaPosicion()
        {
            return ultimaPosicion;
        }

    }

}
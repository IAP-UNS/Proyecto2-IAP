using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modelo
{

    public class LogicSnake
    {
        private List<Vector3Int> snakePositions;

        GameManager gameManager;

        private int currentDirection = 0;


        public void InitSnake(GameManager gm)
        {
            gameManager = gm;
            snakePositions = new List<Vector3Int>();
        }

        public void MoveRight()
        {
            currentDirection = 1;
        }

        public void MoveUp()
        {
            currentDirection = 2;
        }

        public void MoveLeft()
        {
            currentDirection = 3;
        }

        

        public void MoveDown()
        {
            currentDirection = 4;
        }

        public void UpdateSnake()
        {
            FreeSnakeTailPosition();
            UpdateSnakePositionsWhileMoving();
            Vector3Int currentHead = GetHead();

            if (currentDirection == 1)
            {
                currentHead.x += 1;
            }
            else if (currentDirection == 2)
            {
                currentHead.z += 1;
            }
            else if (currentDirection == 3)
            {
                currentHead.x -= 1;
            }
            else if (currentDirection == 4)
            {
                currentHead.z -= 1;
            }

            UpdateSnakeHeadPosition(currentHead);

            
        }

        private void FreeSnakeTailPosition()
        {
            gameManager.FreeSnakePartAt(GetTail().x, GetTail().z);
        }



        public void UpdateSnakeHeadPosition(Vector3Int nuevaPos)
        {
            UpdateHeadPosition(nuevaPos);
            
        }

        public void UpdateSnakeHeadInMatrix()
        {
            gameManager.PlaceSnakePartAt(GetHead().x, GetHead().z);
        }

        public void UpdateHeadPosition(Vector3Int newPosition)
        {
            snakePositions[snakePositions.Count - 1] = newPosition;
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

        public bool IsMoving()
        {
            return currentDirection != 0;
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



    }

}
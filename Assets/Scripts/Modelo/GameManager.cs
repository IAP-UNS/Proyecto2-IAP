using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vista;

namespace Modelo
{



public class GameManager : MonoBehaviour
{
    private bool endOfGame = false;
    

    public GraphicManager graphicManager;
    public InputManager inputManager;

    private LogicSnake logicSnake;

    private Map map;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Execute", inputManager.GetSpeed());
        InitMap();
        InitSnake();
        InitFruit();
        InitInputManger();    
    }

        public void InitMap()
        {
            map = new MapCreator().CreateMap();
            graphicManager.CreateGraphicWalls(map.GetWallsPositions());
        }

        public void InitSnake()
        {
            logicSnake = new LogicSnake();
            logicSnake.InitSnake(this);
            Vector3Int freePosition = map.GetRandomFreePosition();
            UpdateSnakeMatrix(freePosition);
            graphicManager.CreateGraphicSnake(freePosition);
        }

        public void InitFruit()
        {
            Vector3Int freePosition = map.GetRandomFreePosition();
            map.AddEntityAt(new FruitEntity(), freePosition.x, freePosition.z);
            graphicManager.InitGraphicFruit();
            graphicManager.CreateGraphicFruit(freePosition);
        }

        public void InitInputManger()
        {
            inputManager.SetLogicSnake(logicSnake);
        }



        private void Execute()
        {
            logicSnake.UpdateSnake();

            graphicManager.UpdateSnakeGraphics();

            CheckCollisions();

            logicSnake.UpdateSnakeHeadInMatrix();

            //vuelve a llamar a ejecutar 
            if (!endOfGame) Invoke("Execute", inputManager.GetSpeed());
        }









        public void FruitGrabbed()
        {
            graphicManager.FruitGrabbed(logicSnake.GetTail());
        }

        public void GameOver()
        {
            endOfGame = true;
            graphicManager.ActivateEndGame();
        }







    public Vector3Int GetFreePosition()
    {
        return map.GetRandomFreePosition();
    }


    public void PlaceSnakePartAt(int r, int c)
    {
        //map.PlaceSnakePartAt(r, c);
        map.AddEntityAt(new SnakeEntity(), r, c);

    }
    public void FreeSnakePartAt(int r, int c)
    {
        //map.FreeSnakePartAt(r, c);
            map.EmptyCellAt(r, c);
    }

    public void PlaceFrutitaAt(int r, int c)
    {
            map.AddEntityAt(new FruitEntity(), r, c);
    }

    public void FreeFruitAt(int r, int c)
    {
           // map.FreeFruitAt(r, c);
            map.EmptyCellAt(r, c);
    }












    public void CheckCollisions()
    {
            if (logicSnake.IsMoving())
            {
                Vector3Int headPosition = logicSnake.GetHead();
                Cell currentCell = map.GetCellAt(headPosition.x, headPosition.z);
                currentCell.Collision(this);
            }

    }












    private void UpdateSnakeMatrix(Vector3Int nuevaPos)
    {
        logicSnake.AddNewPart(nuevaPos);
        //map.PlaceSnakePartAt(nuevaPos.x, nuevaPos.z);
            map.AddEntityAt(new SnakeEntity(), nuevaPos.x, nuevaPos.z);
    }






    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }


    public void PressedSpaceUp()
    {
        inputManager.SetSpeed(0.05f);
    }

    public void releasedSpaceUp()
    {
        inputManager.SetSpeed(0.2f);
    }



    




    public Vector3Int GetSnakePartPosition(int i)
    {
        return logicSnake.GetSnakePartPosition(i);
    }
    public void UpdateTailPosition(Vector3Int newPosition)
    {
        logicSnake.UpdateTailPosition(newPosition);
    }

}

}
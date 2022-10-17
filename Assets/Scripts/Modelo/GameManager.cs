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
    }


        public void InitMap()
        {
            map = new MapCreator().CreateMap();
            RenderWalls();
        }

        public void InitSnake()
        {
            logicSnake = new LogicSnake();
            logicSnake.InitSnake(this);
            graphicManager.InitSnake();
            Vector3Int nuevaPos = map.GetFreePosition();
            graphicManager.AddNewSnakeGraphicPart(nuevaPos);
            UpdateSnakeMatrix(nuevaPos);
        }

        public void InitFruit()
        {
            Vector3Int nuevaPos3 = map.GetFreePosition();
            graphicManager.InitFruit(nuevaPos3);
            map.PlaceFrutitaAt(nuevaPos3.x, nuevaPos3.z);

            graphicManager.EstirarPlano();
        }


        public void RenderWalls()
        {
            List<Vector3Int> wallsPositions = map.GetWallsPositions();
            foreach(Vector3Int w in wallsPositions)
            {
                graphicManager.CreateGraphicWall(w);
            }
        }




    // Update is called once per frame
    void Update()
    {
        inputManager.UpdatePlayer();

        graphicManager.MoveCamera(logicSnake.GetHead());

    }

    public Vector3Int GetFreePosition()
    {
        return map.GetFreePosition();
    }


    public void PlaceSnakePartAt(int r, int c)
    {
        map.PlaceSnakePartAt(r, c);
    }
    public void FreeSnakePartAt(int r, int c)
    {
        map.FreeSnakePartAt(r, c);
    }

    public void PlaceFrutitaAt(int r, int c)
    {
            map.PlaceFrutitaAt(r, c);
    }

    public void FreeFruitAt(int r, int c)
    {
            map.FreeFruitAt(r, c);
    }

    private void Execute()
    {
            //nuevaPos es la posición nueva de la cabeza
        Vector3Int nuevaPos= logicSnake.UpdateSnake();

        

        logicSnake.UpdateSnakeHeadPosition(nuevaPos);
        graphicManager.UpdateSnakeGraphicsWhileMoving();

            CheckCollisions();

            graphicManager.CheckCurrentFruit(logicSnake.GetUltimaPosicion());

        //vuelve a llamar a ejecutar 
        if (!endOfGame) Invoke("Execute", inputManager.GetSpeed());
    }










    public void CheckCollisions()
    {
        if (CheckFruitCollision(logicSnake.GetHead()))
        {
            graphicManager.DestroyFruit();
            graphicManager.CreateNewFruit();
        }
        if (CheckWallsCollision(logicSnake.GetHead())) //agregar colision con la serpiente
        {
            endOfGame = true;
            graphicManager.ActivateEndGame();
        }
    }












    private void UpdateSnakeMatrix(Vector3Int nuevaPos)
    {
        logicSnake.AddNewPart(nuevaPos);
        map.PlaceSnakePartAt(nuevaPos.x, nuevaPos.z);
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

    public int GetCurrentDirection()
    {
        return inputManager.GetCurrentDirection();
    }


    






    public bool CheckWallsCollision(Vector3Int snakeHead)
    {
        return map.IsWallAt(snakeHead.x, snakeHead.z);
    }

    public bool CheckFruitCollision(Vector3Int snakeHead)
    {
        return map.IsFruitAt(snakeHead.x, snakeHead.z);
    }

    public bool CheckSnakeCollision(Vector3Int snakeHead)
    {
        return map.IsSnakeAt(snakeHead.x, snakeHead.z);
    }


    ///////////////////////////////////////////////
    ///Player input stuff
    ///////////////////////////////////////////////
    ///
    

    


    ///////////////////////////////////////////////
    ///Snake stuff
    ///////////////////////////////////////////////
    ///
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
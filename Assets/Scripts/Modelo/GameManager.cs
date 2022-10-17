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

            inputManager.SetLogicSnake(logicSnake);
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
            Vector3Int freePosition = map.GetFreePosition();
            graphicManager.InitFruit(freePosition);
            // map.PlaceFrutitaAt(nuevaPos3.x, nuevaPos3.z);
            map.AddEntityAt(new FruitEntity(), freePosition.x, freePosition.z);

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

        public void GetFruit()
        {
            graphicManager.DestroyFruit();
            graphicManager.CreateNewFruit();
        }

        public void GameOver()
        {
            endOfGame = true;
            graphicManager.ActivateEndGame();
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

    private void Execute()
    {
            //nuevaPos es la posición nueva de la cabeza
        logicSnake.UpdateSnake();

        graphicManager.UpdateSnakeGraphicsWhileMoving();

            CheckCollisions();

            logicSnake.UpdateSnakeHeadInMatrix();

            graphicManager.CheckCurrentFruit(logicSnake.GetTail());

        //vuelve a llamar a ejecutar 
        if (!endOfGame) Invoke("Execute", inputManager.GetSpeed());
    }










    public void CheckCollisions()
    {
            if (logicSnake.IsMoving())
            {
                Vector3Int headPosition = logicSnake.GetHead();
                Cell currentCell = map.GetCellAt(headPosition.x, headPosition.z);
                currentCell.Collision(this);
            }
            /*
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
            */
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



    





        /*
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
        */

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
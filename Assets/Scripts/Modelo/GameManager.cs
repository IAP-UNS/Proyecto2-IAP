using UnityEngine;
using UnityEngine.SceneManagement;
using Vista;

namespace Modelo
{



public class GameManager : MonoBehaviour
{
    private bool endOfGame = false;
    private Cell[,] matrix;

    public GraphicManager graphicManager;
    public InputManager inputManager;

    private LogicSnake logicSnake;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Execute", inputManager.GetSpeed());

        //init world map
        matrix = new Cell[40, 40];
        for (int i = 0; i < 40; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                matrix[i, j] = new Cell();
            }
        }

        //init walls
        for (int i = 0; i < 40; i++)
        {
            CreateWall(new Vector3Int(0, 0, i));
            CreateWall(new Vector3Int(40 - 1, 0, i));
        }

        for (int j = 0; j < 40; j++)
        {
            CreateWall(new Vector3Int(j, 0, 0));
            CreateWall(new Vector3Int(j, 0, 40 - 1));
        }
        int numberOfRandomWalls = RandomWallValue();
        for (int i = 0; i < numberOfRandomWalls; i++)
        {
            Vector3Int nuevaPos2 = GetFreePosition();
            CreateWall(nuevaPos2);
        }

        //init snake
        logicSnake = new LogicSnake();
        logicSnake.InitSnake(this);
        graphicManager.InitSnake();
        Vector3Int nuevaPos = GetFreePosition();
        graphicManager.AddNewSnakeGraphicPart(nuevaPos);
        UpdateSnakeMatrix(nuevaPos);

        //init fruit
        Vector3Int nuevaPos3 = GetFreePosition();
        graphicManager.InitFruit(nuevaPos3);
        PlaceFrutitaAt(nuevaPos3.x, nuevaPos3.z);

        graphicManager.EstirarPlano();
    }







    // Update is called once per frame
    void Update()
    {
        inputManager.UpdatePlayer();

        graphicManager.MoveCamera(logicSnake.GetHead());

    }







    private void Execute()
    {
        Vector3Int nuevaPos= logicSnake.UpdateSnake();

        CheckCollisions();

        logicSnake.UpdateSnakeHeadPosition(nuevaPos);
        graphicManager.UpdateSnakeGraphicsWhileMoving();


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







    private void CreateWall(Vector3Int nuevaPos)
    {
        graphicManager.CreateGraphicWall(nuevaPos);
        PlaceWallAt(nuevaPos.x, nuevaPos.z);
    }




    private void UpdateSnakeMatrix(Vector3Int nuevaPos)
    {
        logicSnake.AddNewPart(nuevaPos);
        PlaceSnakePartAt(nuevaPos.x, nuevaPos.z);
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


    public void PlaceSnakePartAt(int r, int c)
    {
        matrix[r, c].Snake = (new SnakeEntity());
        //matrixSnake[r, c] = 1;
    }

    public void PlaceWallAt(int r, int c)
    {
        matrix[r, c].Wall = (new WallEntity());
        //matrixWalls[r, c] = 1;
    }

    public void PlaceFrutitaAt(int r, int c)
    {
        matrix[r, c].Fruit = (new FruitEntity());
        //matrixFrutitas[r, c] =  1;
    }




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





    public void FreeSnakePartAt(int r, int c)
    {
        matrix[r, c].Snake = null;
    }

    public void FreeFruitAt(int r, int c)
    {
        matrix[r, c].Fruit = null;
    }





    public int RandomWallValue()
    {
        return Random.Range(50, 50);
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
                isAvailable = !IsWallAt(i, j);
            }
        }
        return isAvailable;
    }

    public bool CheckWallsCollision(Vector3Int snakeHead)
    {
        return IsWallAt(snakeHead.x, snakeHead.z);
    }

    public bool CheckFruitCollision(Vector3Int snakeHead)
    {
        return IsFruitAt(snakeHead.x, snakeHead.z);
    }

    public bool CheckSnakeCollision(Vector3Int snakeHead)
    {
        return IsSnakeAt(snakeHead.x, snakeHead.z);
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
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

        //init world map
        map = new Map();

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
        int numberOfRandomWalls = map.RandomWallValue();
        for (int i = 0; i < numberOfRandomWalls; i++)
        {
            Vector3Int nuevaPos2 = map.GetFreePosition();
            CreateWall(nuevaPos2);
        }

        //init snake
        logicSnake = new LogicSnake();
        logicSnake.InitSnake(this);
        graphicManager.InitSnake();
        Vector3Int nuevaPos = map.GetFreePosition();
        graphicManager.AddNewSnakeGraphicPart(nuevaPos);
        UpdateSnakeMatrix(nuevaPos);

        //init fruit
        Vector3Int nuevaPos3 = map.GetFreePosition();
        graphicManager.InitFruit(nuevaPos3);
        map.PlaceFrutitaAt(nuevaPos3.x, nuevaPos3.z);

        graphicManager.EstirarPlano();
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
        map.PlaceWallAt(nuevaPos.x, nuevaPos.z);
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
using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vista
{

    public class GraphicManager : MonoBehaviour
    {


        public GameObject wallGraphic, snakeGraphic, fruitGraphic;

        public GameObject panelPerdiste;

        public GameObject camera;
        public GameObject gameFloor;
       
        public GameManager gameManager;

        private GraphicSnake graphicSnake;
        private GraphicFruit graphicFruit;

        private void Start()
        {
            EstirarPlano();
        }

        public void EstirarPlano()
        {
            gameFloor.transform.localScale = new Vector3(4, 1, 4);
        }


        private void Update()
        {
            MoveCamera(graphicSnake.GetHead());
        }


        public void MoveCamera(Vector3 snakeHead)
        {
            snakeHead.z -= 15;
            snakeHead.y = 20;
            camera.transform.position = Vector3.Lerp(camera.transform.position, snakeHead, 1 * Time.deltaTime);
        }


        public void CreateGraphicFruit(Vector3Int nuevaPos)
        {
            graphicFruit.CreateGraphicFruit(nuevaPos,fruitGraphic);
        }

        public void FruitGrabbed(Vector3Int tailPosition)
        {
            Vector3Int currentFruitPosition = graphicFruit.GetFruitPosition();
            gameManager.FreeFruitAt(currentFruitPosition.x, currentFruitPosition.z);
            graphicFruit.DestroyFruit();

            Vector3Int nuevaPos = gameManager.GetFreePosition();
            graphicFruit.CreateGraphicFruit(nuevaPos, fruitGraphic);
            gameManager.PlaceFrutitaAt(nuevaPos.x, nuevaPos.z);

            graphicSnake.AgregarElementoGraficoAlFinalDeLaColaDeLaSerpiente(gameManager, snakeGraphic, tailPosition);

        }



        public void ActivateEndGame()
        {
            panelPerdiste.SetActive(true);
        }


        public void CreateGraphicWall(Vector3Int nuevaPos)
        {
            Instantiate(wallGraphic, nuevaPos, transform.rotation);
        }



        public void CreateGraphicSnake(Vector3Int nuevaPos)
        {
            graphicSnake = new GraphicSnake();
            graphicSnake.InitSnake();
            graphicSnake.AddNewSnakeGraphicPart(snakeGraphic, nuevaPos);
        }

        public void UpdateSnakeGraphics()
        {
            graphicSnake.UpdateSnakeGraphicsWhileMoving(gameManager);
        }

        public void InitGraphicFruit()
        {
            graphicFruit = new GraphicFruit();
        }

        public void CreateGraphicWalls(List<Vector3Int> wallsPositions)
        {
            //List<Vector3Int> wallsPositions = map.GetWallsPositions();
            foreach (Vector3Int w in wallsPositions)
            {
                CreateGraphicWall(w);
            }
        }

    }


}

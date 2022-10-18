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
        private GameObject currentFruit;
        public GameObject camera;
        public GameObject gameFloor;
        private bool gotFrutita; //indica que agarró una frutita para agrandar la cola

        public GameManager gameManager;

        private GraphicSnake graphicSnake;

        private void Start()
        {
            EstirarPlano();
        }

        private void Update()
        {
            MoveCamera(graphicSnake.GetHead());
        }

        public void CreateGraphicFruit(Vector3Int nuevaPos3)
        {
            currentFruit = Instantiate(fruitGraphic, nuevaPos3, transform.rotation) as GameObject;
        }

        public void EstirarPlano()
        {
            gameFloor.transform.localScale = new Vector3(4, 1, 4);
        }


        public void MoveCamera(Vector3 snakeHead)
        {
            snakeHead.z -= 15;
            snakeHead.y = 20;
            camera.transform.position = Vector3.Lerp(camera.transform.position, snakeHead, 1 * Time.deltaTime);
        }




        public void DestroyFruit()
        {
            int r = (int)currentFruit.transform.position.x;
            int c = (int)currentFruit.transform.position.z;
            gameManager.FreeFruitAt(r, c);
            Destroy(currentFruit);
        }

        public void CreateNewFruit()
        {
            Vector3Int nuevaPos3 = gameManager.GetFreePosition();
            currentFruit = Instantiate(fruitGraphic, nuevaPos3, transform.rotation) as GameObject;
            gameManager.PlaceFrutitaAt(nuevaPos3.x, nuevaPos3.z);
            gotFrutita = true;
        }

  
        public void CheckCurrentFruit(Vector3Int ultimaPosicion)
        {
            //si agarró frutita se añade un pedaso más a la cola de la serpiente
            if (gotFrutita)
            {
                gotFrutita = false;
                graphicSnake.AgregarElementoGraficoAlFinalDeLaColaDeLaSerpiente(gameManager, snakeGraphic, ultimaPosicion);
            }
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

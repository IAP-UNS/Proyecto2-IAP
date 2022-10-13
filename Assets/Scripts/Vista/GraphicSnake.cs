using Modelo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vista
{

    public class GraphicSnake
    {
        private List<GameObject> snakeGraphicParts;


        public void InitSnake()
        {
            snakeGraphicParts = new List<GameObject>();
        }

        public void UpdateSnakeGraphicsWhileMoving(GameManager gameManager)
        {
            for (int i = 0; i < snakeGraphicParts.Count; i++)
            {
                snakeGraphicParts[i].transform.position = gameManager.GetSnakePartPosition(i);
            }
        }

        public void AgregarElementoGraficoAlFinalDeLaColaDeLaSerpiente(GameManager gameManager, GameObject snakeGraphic, Vector3Int ultimaPosicion)
        {
            gameManager.UpdateTailPosition(ultimaPosicion);
            GameObject nuevoCubito = GameObject.Instantiate(snakeGraphic, ultimaPosicion, snakeGraphic.transform.rotation);
            snakeGraphicParts.Insert(0, nuevoCubito);

            gameManager.PlaceSnakePartAt(ultimaPosicion.x, ultimaPosicion.z);
        }

        public void AddNewSnakeGraphicPart(GameObject snakeGraphic, Vector3Int nuevaPos)
        {
            GameObject nuevoSnakePart = GameObject.Instantiate(snakeGraphic, nuevaPos, snakeGraphic.transform.rotation) as GameObject;
            snakeGraphicParts.Add(nuevoSnakePart);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vista;

public class GraphicFruit 
{
    private GameObject currentFruit;

    public void CreateGraphicFruit(Vector3Int nuevaPos, GameObject fruitGraphic)
    {
        currentFruit = GameObject.Instantiate(fruitGraphic, nuevaPos, Quaternion.identity) as GameObject;
    }

    public void DestroyFruit()
    {
        GameObject.Destroy(currentFruit);
    }


    public Vector3Int GetFruitPosition()
    {
        int r = (int)currentFruit.transform.position.x;
        int c = (int)currentFruit.transform.position.z;
        Vector3Int currentPosition= new Vector3Int(r,0,c);
        return currentPosition;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator 
{

    public Map CreateMap()
    {
        Map newMap = new Map();
        
        //init walls
        for (int i = 0; i < 40; i++)
        {
            CreateWall(newMap,new Vector3Int(0, 0, i));
            CreateWall(newMap, new Vector3Int(40 - 1, 0, i));
        }

        for (int j = 0; j < 40; j++)
        {
            CreateWall(newMap, new Vector3Int(j, 0, 0));
            CreateWall(newMap, new Vector3Int(j, 0, 40 - 1));
        }
        int numberOfRandomWalls = newMap.RandomWallValue();
        for (int i = 0; i < numberOfRandomWalls; i++)
        {
            Vector3Int nuevaPos2 = newMap.GetFreePosition();
            CreateWall(newMap, nuevaPos2);
        }


        return newMap;
    }

    private void CreateWall(Map map, Vector3Int nuevaPos)
    {
        //map.PlaceWallAt(nuevaPos.x, nuevaPos.z);
        map.AddEntityAt(new WallEntity(), nuevaPos.x, nuevaPos.z);
    }

  

}

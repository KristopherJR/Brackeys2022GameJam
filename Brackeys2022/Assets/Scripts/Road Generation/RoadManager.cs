using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadManager : MonoBehaviour
{

    [SerializeField]
    private List<Road> roads;
    
    [SerializeField, Range(0,100)]
    private int roadsToGenerate;

    [SerializeField, HideInInspector]
    private Road lastRoad;




    [ContextMenu(nameof(GenerateRoads))]
    public void GenerateRoads()
    {
        for (int i = 0; i < roadsToGenerate; i++)
        {
            CreateRoad();
        }
        
    }
    
    
    public void CreateRoad()
    {
        if (roads.Count == 0) throw new Exception("No roads");
        
        Road randomRoad = roads[Random.Range(0, roads.Count)];
        Vector3 pos = lastRoad != null ? lastRoad.EndPosition.transform.position : Vector3.zero;
        Quaternion rot = lastRoad != null ? lastRoad.EndPosition.transform.rotation : Quaternion.identity;
        
        Road newObject = Instantiate(randomRoad, pos, rot, transform);

        lastRoad = newObject;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadManager : MonoBehaviour
{

    [SerializeField]
    private List<Road> roads;


    [ContextMenu(nameof(CreateRoad))]
    public void CreateRoad()
    {
        if (roads.Count == 0) return;
        
        Road randomRoad = roads[Random.Range(0, roads.Count)];
        Instantiate(randomRoad, transform);
        
        
    }
}

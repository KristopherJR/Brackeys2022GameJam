using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    [SerializeField]
    private GameObject endPosition;
    
    [SerializeField]
    private GameObject startPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(endPosition.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

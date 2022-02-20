using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Road : MonoBehaviour
{

    [field: SerializeField]
    public Transform EndPosition { get; set; }
    
    [field: SerializeField]
    public Transform StartPosition  { get; set; }
    
}

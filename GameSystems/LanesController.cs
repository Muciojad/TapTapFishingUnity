using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesController : MonoBehaviour
{
    /// <summary>
    /// Array of lanes. [0] is the top one
    /// </summary>
    [SerializeField] private Transform[] _lanes;

    public static LanesController instance;
    private void Awake()
    {
        instance = this;
    }
    
    public Vector3 getLanePosition(int laneFromTop)
    {
        return _lanes[laneFromTop].position;
    }  
}

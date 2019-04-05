using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The best thing in the world - object pooler.
/// Allows to reuse given objects instead of messing with instantiating things over and over again
/// </summary>
public class ObjectPooler : MonoBehaviour {
    /// <summary>
    /// Object to pool
    /// </summary>
    [SerializeField] private GameObject pooledObject;
    /// <summary>
    /// Starting amount of pre-instantiated objects
    /// </summary>
    [SerializeField] private int pooledAmount;
    /// <summary>
    /// If fixed, return null when pooler can't find object ready to reuse, if no extend list of objects
    /// </summary>
    [SerializeField] private bool isFixed;
    [SerializeField] private List<GameObject> objects;

    //public static ObjectPooler objectPooler;
    public ObjectPooler objectPooler;
    // Use this for initialization

    private void Awake()
    {
        objectPooler = this;
    }

    /// <summary>
    /// Make sure that all objects in pool will be inactive after disable
    /// </summary>
    private void OnDisable()
    {
        foreach(var obj in objects)
        {
            if(obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
    void Start () {
        objects = new List<GameObject>();
        for(int i = 0; i< pooledAmount; i++)
        {
            objects.Add(Instantiate(pooledObject));
            objects[objects.Count - 1].SetActive(false);
        }
	}
	
    /// <summary>
    /// Serve object from pool.
    /// </summary>
    /// <returns></returns>
    public GameObject Get()
    {
        for(int i = 0; i< objects.Count; i++)
        {
            if(objects[i] != null)
            {
                if (!objects[i].activeInHierarchy)
                {
                    return objects[i];
                }
            }            
        }
        if(!isFixed)
        {
            objects.Add(Instantiate(pooledObject));
            return objects[objects.Count - 1];
        }
        return null;
    }
}

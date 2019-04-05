using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dispatch bubble particles.
/// </summary>
public class BubbleSplashDispatcher : MonoBehaviour
{
    public static BubbleSplashDispatcher instance;

    [SerializeField] private ObjectPooler splashPooler;
    private void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// Adds single bubble particle.
    /// </summary>
    /// <param name="position"></param>
    public void AddBubbleSplash(Vector3 position)
    {
        var bs = splashPooler.Get();
        if(bs != null)
        {
            bs.transform.position = position;
            bs.SetActive(true);
        }
    }

}

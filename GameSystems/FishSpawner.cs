using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deals with fish spawning.
/// </summary>
public class FishSpawner : MonoBehaviour
{
    /// <summary>
    /// Assign fish object poolers.
    /// </summary>
    [SerializeField] private ObjectPooler[] _fishPoolers;
    [Space]
    // map difficulty level to time between next fish spawn
    [SerializeField] private float[] difficultyToRoundTimeMap;


    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        CancelInvoke();
        InvokeRepeating("SpawnFish", 1f, difficultyToRoundTimeMap[GameController.instance.DifficultyLevel]);
    }

    /// <summary>
    /// Spawns single fish.
    /// </summary>
    private void SpawnFish()
    {
        var fish = AdvancedFishDraw();
        if(fish != null)
        {
            fish.transform.position = new Vector3(GameBordersController.instance.getRightBorder().x ,LanesController.instance.getLanePosition(Random.Range(0,3)).y);
            fish.transform.SetParent(GameController.instance.transform);
            fish.SetActive(true);
        }
    }

    /// <summary>
    /// Get fish to spawn from random pool.
    /// Most probably green will be spawned, then blue, red with smallest chance
    /// </summary>
    /// <returns></returns>
    private GameObject AdvancedFishDraw()
    {
        var randomPooler = Random.Range(0, 10);
        if(randomPooler < 6)
        {
            return _fishPoolers[0].Get();
            //green
        }
        else if(randomPooler >= 6 && randomPooler < 8)
        {
            return _fishPoolers[1].Get();
        }
        else
        {
            return _fishPoolers[2].Get();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodManager : MonoBehaviour
{
    /// <summary>
    /// Assign all rods to this array.
    /// </summary>
    [SerializeField] private GameObject[] rodList;
    /// <summary>
    /// When enabling, activate just number of rods specified in game controller
    /// </summary>
    private void OnEnable()
    {
        for(int i = 0; i< GameController.instance.ActiveRods; i++)
        {
            rodList[i].gameObject.SetActive(true);
        }
    }
}

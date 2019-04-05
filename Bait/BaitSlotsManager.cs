using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grabs all bait slots together and give specified one if necessary.
/// </summary>
public class BaitSlotsManager : MonoBehaviour
{
    [SerializeField] private Transform[] _baitSlots;

    public Vector3 getBaitSlot(int slotIdFromTop)
    {
        return _baitSlots[slotIdFromTop].position;
    }
}

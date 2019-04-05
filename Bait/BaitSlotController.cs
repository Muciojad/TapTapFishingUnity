using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls single bait slot.
/// </summary>
public class BaitSlotController : MonoBehaviour
{
    private class SlotClickNotifier : Subject {}
    private SlotClickNotifier _clickNotifier = new SlotClickNotifier();
    

    [SerializeField] private int _selfId;
    [SerializeField] private BaitController _baitController;
    [SerializeField] private RodController _rodController;

    private bool _mouseOver = false;

    private void Awake()
    {
        _clickNotifier.AddObserver(_baitController.GetObserver());
        _clickNotifier.AddObserver(_rodController.GetObserver());
    }

    private void Update()
    {
        // if mouse is over slot and mouse button is down -> that means bait is clicked, send notification to listeners
        if(Input.GetMouseButtonDown(0) && _mouseOver)
        {
            // send notification to bait listener
            _clickNotifier.Notify(new OnBaitSlotClicked() { slotId = _selfId });
        }
    }

    /// <summary>
    /// Check if mouse object is over the slot
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.CompareTo("Pointer") == 0)
        {
            _mouseOver = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.CompareTo("Pointer") == 0)
        {
            _mouseOver = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bait controller.
/// Manages position changes, collisions.

/// </summary>
public class BaitController : MonoBehaviour
{
    [SerializeField] private BaitSlotsManager _baitSlotsManager;

    /// <summary>
    /// Observer listening for OnBaitSlotClicked event.
    /// When event is registered, fires it's delegate method.
    /// </summary>
    private class BaitSlotClickObserver : Observer
    {
        public delegate void OnNotifyDelegate(int param);
        public OnNotifyDelegate onNotifyDelegate;

        public override void OnNotify(object @event)
        {
            if(@event is OnBaitSlotClicked)
            {
                var data = (OnBaitSlotClicked)@event;
                onNotifyDelegate(data.slotId);
            }
        }
    }
    private BaitSlotClickObserver _clickObserver = new BaitSlotClickObserver();

    /// <summary>
    /// Notifier sending OnRodBreak event.
    /// </summary>
    private class RodBreakNotifier : Subject { }
    private RodBreakNotifier _rodBreakNotifier = new RodBreakNotifier();

    void TryToGetGC()
    {
      _rodBreakNotifier.AddObserver(GameController.instance.GetRodBreakObserver());
    }

    private void Awake()
    {
        TryToGetGC();

        _clickObserver.onNotifyDelegate = SwitchBait;
    }
    private void OnEnable()
    {
        SwitchBait(0);
    }

    /// <summary>
    /// Change position of bait.
    /// </summary>
    /// <param name="newBaitSlot"></param>
    void SwitchBait(int newBaitSlot)
    {
        transform.position = _baitSlotsManager.getBaitSlot(newBaitSlot);
    }

    /// <summary>
    /// Provides bait's observer object for subjects.
    /// </summary>
    /// <returns></returns>
    public Observer GetObserver()
    {
        return _clickObserver;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.CompareTo("RedFish") == 0)
        {
            transform.parent.gameObject.SetActive(false);
            _rodBreakNotifier.Notify(new OnRodBreak());
        }
    }
}

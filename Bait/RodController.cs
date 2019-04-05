using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{
    [SerializeField] private BaitSlotsManager slotsManager;
    [SerializeField] private float[] adjustYPosToBait;
    private Vector3 basePosition;
    private Vector3 constOffset = new Vector3(0, .25f);

    private void Awake()
    {
        basePosition = transform.position;

        _clickObserver.onNotifyDelegate += AdjustToBaitSlot;
    }

    private void OnEnable()
    {
        AdjustToBaitSlot(0);
    }
    void Start()
    {
    }

    void AdjustToBaitSlot(int baitSlot)
    {
        transform.position = new Vector3(transform.position.x, adjustYPosToBait[baitSlot]);
    }

    public Observer GetObserver()
    {
        return _clickObserver;
    }

    private class BaitSlotClickObserver : Observer
    {
        public delegate void OnNotifyDelegate(int param);
        public OnNotifyDelegate onNotifyDelegate;

        public override void OnNotify(object @event)
        {
            if (@event is OnBaitSlotClicked)
            {
                var data = (OnBaitSlotClicked)@event;
                onNotifyDelegate(data.slotId);
            }
        }
    }
    private BaitSlotClickObserver _clickObserver = new BaitSlotClickObserver();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays reward messages on game view.
/// </summary>
public class RewardUIManager : MonoBehaviour
{

    public static RewardUIManager instance;

    /// <summary>
    /// Message is displayed when player catch fish.
    /// 3 kinds of messages could be assigned - when catch is good(green fish), neutral(blue) or bad(red).
    /// </summary>

    [SerializeField] private string[] _messagesGood, _messagessNeutral, _messagesBad;
    [SerializeField] private TMPro.TMP_Text _messageRenderText;
    [Space]
    [SerializeField] private Color _goodMessageColor;
    [SerializeField] private Color _badMessageColor;


    private class RewardCollectedObserver : Observer
    {
        public delegate void OnNotifyDelegate(int param);
        public OnNotifyDelegate onNotifyDelegate;

        public override void OnNotify(object @event)
        {
            if (@event is OnRewardCollected)
            {
                var data = (OnRewardCollected)@event;
                onNotifyDelegate(data.amount);
            }
        }
    }
    private RewardCollectedObserver _rewardCollectedObserver = new RewardCollectedObserver();

    public Observer GetObserver()
    {
        return _rewardCollectedObserver;
    }

    private void Awake()
    {
        instance = this;
        _rewardCollectedObserver.onNotifyDelegate += DisplayReward;
    }

    private void OnEnable()
    {
        _messageRenderText.text = "";
    }

    /// <summary>
    /// Display reward on screen.
    /// </summary>
    /// <param name="amount"></param>
    void DisplayReward(int amount)
    {
        // check what reward amount came to method
        if(amount < 0)
        {
            // catch is "bad" -> display random red message from bad messages
            _messageRenderText.text = _messagesBad[Random.Range(0, _messagesBad.Length)];
            _messageRenderText.color = _badMessageColor;
            SwitchTextPos();
        }

        else if (amount > 0)
        {
            // catch is "good" -> display random green message from good messages
            _messageRenderText.text = _messagesGood[Random.Range(0, _messagesGood.Length)];
            _messageRenderText.color = _goodMessageColor;
            SwitchTextPos();
        }
    }

    /// <summary>
    /// Flip message position on X axis
    /// </summary>
    void SwitchTextPos()
    {
        _messageRenderText.rectTransform.position =
            new Vector3(_messageRenderText.rectTransform.position.x * -1,
            _messageRenderText.rectTransform.position.y,
            _messageRenderText.rectTransform.position.z);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton with score data.
/// </summary>
public class PlayerScore
{
    public static PlayerScore instance = new PlayerScore();

    public PlayerScore()
    {
        _rewardCollectedObserver.onNotifyDelegate += UpdateScore;
    }
    /// <summary>
    /// Nested observer listening for OnReward collected event
    /// </summary>
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

    public int Score => _scoreAmount;
    private int _scoreAmount = 0;

    public void UpdateScore(int amount)
    {
        _scoreAmount += amount;
    }

    public Observer GetObserver()
    {
        return _rewardCollectedObserver;
    }

    /// <summary>
    /// Reset last result.
    /// </summary>
    public void Reset()
    {
        _scoreAmount = 0;
    }
}

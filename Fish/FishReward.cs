using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages fish rewards.
/// </summary>
public class FishReward : MonoBehaviour
{
    private class RewardNotifier : Subject { }
    private RewardNotifier _rewardNotifier = new RewardNotifier();

    /// <summary>
    /// Assign how big or low is reward for catching this kind of fish.
    /// </summary>
    [SerializeField] private int _rewardForFish;
    private void Awake()
    {
        _rewardNotifier.AddObserver(RewardUIManager.instance.GetObserver());
        _rewardNotifier.AddObserver(PlayerScore.instance.GetObserver());
        _rewardNotifier.AddObserver(ScoreUIController.instance.GetObserver());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Bait") == 0)
        {
            _rewardNotifier.Notify(new OnRewardCollected() { amount = _rewardForFish });
            // send notify OnRewardCollected
        }
    }
}

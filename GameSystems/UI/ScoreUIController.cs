using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton controller of Score UI.
/// Allows to update score label.
/// </summary>
public class ScoreUIController : MonoBehaviour
{
    public static ScoreUIController instance;

    /// <summary>
    /// Private nested observer for dealing with collecting reward from fishes.
    /// Simple wait for specified event type and call delegate func from main class.
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

    private TMPro.TMP_Text _scoreLabel;
    private string _scoreText = "Score\n";
    

    private void Awake()
    {
        instance = this;
        _rewardCollectedObserver.onNotifyDelegate += UpdateScore;
        _scoreLabel = GetComponent<TMPro.TMP_Text>();
    }
    private void OnEnable()
    {
        _scoreLabel.text = _scoreText;
    }

    void UpdateScore(int addAmount)
    {
        _scoreLabel.text = _scoreText + PlayerScore.instance.Score.ToString();
    }

    public Observer GetObserver()
    {
        return _rewardCollectedObserver;
    }

}

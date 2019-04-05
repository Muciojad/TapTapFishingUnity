using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScoreScreen singleton controller.
/// Displays score on Score view.
/// </summary>
public class ScoreScreen : MonoBehaviour
{
    public static ScoreScreen instance;
    public Observer GetObserver => _onTransitionObserver;

    [SerializeField] private TMPro.TMP_Text scoreResultLabel;

    private int savedScoreForDisplay = 0;
    private void Awake()
    {
        instance = this;
        _onTransitionObserver.onNotifyDelegate += ManageTransition;
        gameObject.SetActive(false);
    }

    public void SaveScore(int playerScore)
    {
        savedScoreForDisplay = playerScore;
        scoreResultLabel.text = savedScoreForDisplay.ToString();
    }

    /// <summary>
    /// Method used in observer delegate to handle transitions.
    /// </summary>
    /// <param name="transitionTo"></param>
    void ManageTransition(string transitionTo)
    {
        if(transitionTo.CompareTo("ScoreScreen") == 0)
        {
            gameObject.SetActive(true);
        }
    }

    
    // yes, this is awful code copy - it should be in another class wrapped into single file and added as member of this class
    // but you know, it's fast typed on knee between classes, maybe I'll refactor it someday

    private class OnTransitionObserver : Observer
    {
        public delegate void OnNotifyDelegate(string transitionTo);
        public OnNotifyDelegate onNotifyDelegate;

        public override void OnNotify(object @event)
        {
            if (@event is OnTransition)
            {
                var data = (OnTransition)@event;
                onNotifyDelegate(data.transitionToScreenName);
            }
        }
    }
    private OnTransitionObserver _onTransitionObserver = new OnTransitionObserver();
}

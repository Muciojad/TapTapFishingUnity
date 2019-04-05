using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main game controller.
/// Deals with game end, success/failure, difficulty level etc
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController instance;

    /// <summary>
    /// Observer listening for OnGameEnd event.
    /// When notified delegate is fired.
    /// </summary>
    private class OnGameEndObserver : Observer
    {
        public delegate void OnNotifyDelegate(bool param);
        public OnNotifyDelegate onNotifyDelegate;

        public override void OnNotify(object @event)
        {
            if (@event is OnGameEnd)
            {
                var data = (OnGameEnd)@event;
                onNotifyDelegate(data.isGameFailed);
            }
        }
    }
    private OnGameEndObserver _onGameEndObserver = new OnGameEndObserver();

    /// <summary>
    /// Observer listening for OnRodBreak event.
    /// When notified delegate is fired.
    /// </summary>
    private class OnRodBreakObserver : Observer
    {
        public delegate void OnNotifyDelegate();
        public OnNotifyDelegate onNotifyDelegate;

        public override void OnNotify(object @event)
        {
            if (@event is OnRodBreak)
            {
                var data = (OnRodBreak)@event;
                onNotifyDelegate();
            }
        }
    }
    private OnRodBreakObserver _onRodBreakObserver = new OnRodBreakObserver();

    /// <summary>
    /// Observer listening for OnTransition event.
    /// When notified delegate is fired.
    /// </summary>
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

    /// <summary>
    /// Provides amount of active fishing rods. 
    /// </summary>
    public int ActiveRods => _activeRods;
    private int _activeRods = 3;

    /// <summary>
    /// Provides current difficulty level.
    /// </summary>
    public int DifficultyLevel => _difficultyLevel;
    private int _difficultyLevel = 2;

    public Observer GetTransitionObserver => _onTransitionObserver;

    private void Awake()
    {
        instance = this;
        _onGameEndObserver.onNotifyDelegate += ManageGameResult;
        _onRodBreakObserver.onNotifyDelegate += UpdateRods;
        _onTransitionObserver.onNotifyDelegate += ManageTransition;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // Difficulty level is increasing with each round - max is 2 (indexed from 0, so really there are 3 difficulty levels)
        if (_difficultyLevel < 2) _difficultyLevel++;
        _activeRods = 3;
        // reset player result from last game
        PlayerScore.instance.Reset();
    }

    /// <summary>
    /// Deal with registered transition
    /// </summary>
    /// <param name="transitionTo"></param>
    void ManageTransition(string transitionTo)
    {
        if(transitionTo.CompareTo("GameScreen") == 0)
        {
            tmp_ActivateGame();
        }
        else if (transitionTo.CompareTo("ScoreScreen") == 0)
        {
            ScoreScreen.instance.SaveScore(PlayerScore.instance.Score);
        }
    }

    public void tmp_ActivateGame()
    {
        Invoke(nameof(StartGame), .5f);
    }

    void StartGame()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Check game result and if game failed, enable score view.
    /// </summary>
    /// <param name="isGameFailed"></param>
    void ManageGameResult(bool isGameFailed)
    {
        if (isGameFailed && ActiveRods == 0)
        {
            TransitionManager.instance.SetScoresActive();
            // of course notification could be used, but using singleton here it's not so big spaghetting I think
        }
        gameObject.SetActive(false);
    }

    void UpdateRods()
    {
        _activeRods--;
        if(ActiveRods <= 0)
        {
            ManageGameResult(true);
        }
    }
    public Observer GetGameStatusObserver()
    {
        return _onGameEndObserver;
    }
    public Observer GetRodBreakObserver()
    {
        return _onRodBreakObserver;
    }
}

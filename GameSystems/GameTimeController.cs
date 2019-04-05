using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls round time. Singleton.
/// </summary>
public class GameTimeController : MonoBehaviour
{
    public static GameTimeController instance;

    /// <summary>
    /// Assign round time.
    /// </summary>
    [SerializeField] private float _singleGameTime;
    private float _currentTime = 0f;
    /// <summary>
    /// Provides time elapsed from round start.
    /// </summary>
    public float ElapsedTime => _currentTime;
    /// <summary>
    /// Provides round duration.
    /// </summary>
    public float RoundTime => _singleGameTime;

    /// <summary>
    /// Nested notifier - sending OnGameEnd event with isGameFailed flag set to false.
    /// </summary>
    private class OutOfTimeNotifier : Subject    { }
    private OutOfTimeNotifier _outOfTimeNotifier = new OutOfTimeNotifier();

    private bool _observerFilled = false;

    private void GetGC_Observer()
    {
            _outOfTimeNotifier.AddObserver(GameController.instance.GetGameStatusObserver());
            _observerFilled = true;
    }


    private void Awake()
    {
        instance = this;
        GetGC_Observer();
    }

    private void OnDisable()
    {
        _currentTime = 0f;
    }
    

    void Update()
    {
        if(_currentTime < _singleGameTime)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            _outOfTimeNotifier.Notify(new OnGameEnd() { isGameFailed = false });
            TransitionManager.instance.SetScoresActive();
            _currentTime = -1;
            // player successfully fished till end of round
        }

    }    
}

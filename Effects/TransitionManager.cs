using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Deals with transitions between views -> game view, main menu view and score view. Singleton.
/// </summary>
public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance { get; private set; }

    /// <summary>
    /// Assign specified transforms of screens, rect transforms of canvases
    /// and final destinations - where should be hidden left & right part of UI, and where should be shown middle part
    /// </summary>

    [SerializeField] private Transform mainMenuScreen, gameScreen;
    [SerializeField] private Vector3 leftScreenPos, midScreenPos, rightScreenPos;

    [Space]
    [SerializeField] private RectTransform mainMenuCanvas, gameCanvas, scoreCanvas;
    [SerializeField] private Vector3 leftCanvasPos, midCanvasPos, rightCanvasPos, bottomCanvasPos;

    [Space]
    [SerializeField] GameObject GameSectionObject;

    private bool _observerAssigned = false;

    private void Awake()
    {
        instance = this;
        TryAssignObserver();
    }

    /// <summary>
    /// Get observers from other singletons
    /// </summary>
    void TryAssignObserver()
    {
        _transitionNotifier.AddObserver(GameController.instance.GetTransitionObserver);
        _transitionNotifier.AddObserver(ScoreScreen.instance.GetObserver);
        _observerAssigned = true;
    }

    /// <summary>
    /// Deals with transition from main menu/score view to game view.
    /// Could be just called directly from singleton instance
    /// </summary>
    public void SetGameActive()
    {
        mainMenuScreen.DOMove(leftScreenPos, 1f);
        mainMenuCanvas.DOAnchorPos(leftCanvasPos, 1f);

        gameScreen.DOMove(midScreenPos, 1f);
        gameCanvas.DOAnchorPos(midCanvasPos, 1f);

        scoreCanvas.DOAnchorPos(bottomCanvasPos, 1f);

        _transitionNotifier.Notify(new OnTransition() { transitionToScreenName = "GameScreen" });

    }
    /// <summary>
    /// Deals with transition from game view to score view.
    /// Could be just called directly from singleton instance.
    /// </summary>
    public void SetScoresActive()
    {
        mainMenuScreen.DOMove(leftScreenPos, 1f);
        mainMenuCanvas.DOAnchorPos(leftCanvasPos, 1f);

        gameScreen.DOMove(rightScreenPos, 1f);
        gameCanvas.DOAnchorPos(rightCanvasPos, 1f);

        scoreCanvas.DOAnchorPos(midCanvasPos, 1f);

        _transitionNotifier.Notify(new OnTransition() { transitionToScreenName = "ScoreScreen" });
    }

    /// <summary>
    /// Enable main game object.
    /// </summary>
    void EnableGameSection()
    {
        GameSectionObject.SetActive(true);
    }

    /// <summary>
    /// Nested notifier for sending notifs about transitions.
    /// </summary>
    private class TransitionNotifier : Subject { }
    private TransitionNotifier _transitionNotifier = new TransitionNotifier();
   
}

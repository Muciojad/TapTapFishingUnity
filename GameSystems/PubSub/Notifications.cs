using System;

/// <summary>
/// Set of structs representing events.
/// Used by all observers and subjects.
/// </summary>

public struct OnBaitSlotClicked { public int slotId; }
public struct OnRewardCollected { public int amount; }
public struct OnGameEnd { public bool isGameFailed; }
public struct OnRodBreak {  }
public struct OnTransition { public string transitionToScreenName;  }

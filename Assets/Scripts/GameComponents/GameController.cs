using System;
using RFW.Events;
using UnityEngine;

public class GameController: IDisposable
{
    private GameEvents _gameEvents = null;

    public GameController(GameEvents gameEvents)
    {
        _gameEvents = gameEvents;
        _gameEvents.OnGameLoaded += OnGameLoaded;

    }

    private void OnGameLoaded()
    {
        Debug.LogError("GameLoaded");
    }


    public void Dispose()
    {
        _gameEvents.OnGameLoaded -= OnGameLoaded;
        _gameEvents = null;
    }
}
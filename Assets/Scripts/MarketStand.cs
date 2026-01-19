using System;
using UnityEngine;

public class MarketStand : MonoBehaviour
{
    private int _currentTomatoes = 0;

    private void OnEnable()
    {
        GameEvents.TomatoReachedMarket += OnTomatoReached;
    }

    private void OnDisable()
    {
        GameEvents.TomatoReachedMarket -= OnTomatoReached;
    }

    private void OnTomatoReached()
    {
        _currentTomatoes++;
        if (_currentTomatoes == 5)
        {
            GameEvents.AllTomatoesInMarket?.Invoke();
            GameEvents.CameraGlide?.Invoke();
        }
    }
}

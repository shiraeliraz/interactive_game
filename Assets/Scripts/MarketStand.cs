using System;
using DG.Tweening;
using UnityEngine;

public class MarketStand : MonoBehaviour
{
    private int _currentTomatoes = 0;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        GameEvents.TomatoReachedMarket += OnTomatoReached;
        GameEvents.StopDriving += MarketSoundOn;
        GameEvents.TomatoInBag += MarketSoundOff;
        MarketSoundOff();
    }

    private void OnDisable()
    {
        GameEvents.TomatoReachedMarket -= OnTomatoReached;
        GameEvents.StopDriving -= MarketSoundOn;
        GameEvents.TomatoInBag -= MarketSoundOff;
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

    private void MarketSoundOn()
    {
        _audioSource.enabled = true;
    }

    private void MarketSoundOff()
    {
        _audioSource.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                _audioSource.enabled = false;   
                _audioSource.volume = 1f;
            });
    }

}

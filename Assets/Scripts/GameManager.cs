using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalRotPhases;
    [SerializeField] private int timeForSingleRotPhase;
    private int _currentPhase = 0;
    private int _currentTimePassed = 0;
private bool _finished = false;
    private void OnEnable()
    {
        GameEvents.TimePassed += OnTimePassed;
    }

    private void OnDisable()
    {
        GameEvents.TimePassed -= OnTimePassed;
    }

    private void OnTimePassed()
    {
        if (_finished)
        {
            return;
        }
        _currentTimePassed++;
        if (_currentTimePassed >= timeForSingleRotPhase)
        {
            GameEvents.SingleRotPhase?.Invoke();
            _currentTimePassed = 0;
            _currentPhase++;

            if (_currentPhase >= totalRotPhases)
            {
                GameEvents.TomatoIsRotten?.Invoke();
                _finished = true;
                Debug.Log("finished");
            }
        }
    }
}

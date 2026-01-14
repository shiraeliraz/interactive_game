using UnityEngine;
using DG.Tweening;

public class Crate : MonoBehaviour
{
    [SerializeField] private Vector3 tomatoLoadLocation;
    [SerializeField] private float firstMoveDuration = 1f;
    private int _tomatoCounter;
    private Collider2D _collider;

    private void OnEnable()
    {
        transform.DOMove(tomatoLoadLocation, firstMoveDuration);
        GameEvents.TomatoInBasket += GainTomato;
        _collider =  GetComponent<Collider2D>();
        _collider.enabled = false;
        
    }

    private void OnDisable()
    {
        GameEvents.TomatoInBasket -= GainTomato;
    }

    private void GainTomato()
    {
        _tomatoCounter++;
        Debug.Log(_tomatoCounter);
        if (_tomatoCounter == 5)
        {
            GameEvents.CameraGlide.Invoke();
            _collider.enabled = true;
        }
    }

    public void ReachTruck()
    {
        GameEvents.CrateReachedTruck?.Invoke();
    }
}
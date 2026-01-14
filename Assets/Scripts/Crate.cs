using UnityEngine;
using DG.Tweening;

public class Crate : MonoBehaviour
{
    [SerializeField] private Vector3 tomatoLoadLocation;
    [SerializeField] private float firstMoveDuration = 1f;

    private void OnEnable()
    {
        transform.DOMove(tomatoLoadLocation, firstMoveDuration);
    }
}
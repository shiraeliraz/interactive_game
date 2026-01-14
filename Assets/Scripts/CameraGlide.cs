using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraGlide : MonoBehaviour
{
    private int _currentPos = 0;
    [SerializeField] private List<Vector3> positions;
    [SerializeField] private float speed = 0.2f;

    private void OnEnable()
    {
        GameEvents.CameraGlide += GlideCamera;
    }

    private void OnDisable()
    {
        GameEvents.CameraGlide -= GlideCamera;
    }
    private void GlideCamera()
    {
        _currentPos++;
        if (_currentPos < positions.Count)
        {
            transform.DOMove(positions[_currentPos], speed);
        }
        
    }
}

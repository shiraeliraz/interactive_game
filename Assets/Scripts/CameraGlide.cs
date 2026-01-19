using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraGlide : MonoBehaviour
{
    [Serializable]
    public struct CameraInstructions
    {
        public Vector3 pos;
        public float duration;
    }
    private int _currentPos = 0;
    [SerializeField] private List<CameraInstructions> cameraInstructions;

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
        if (_currentPos < cameraInstructions.Count)
        {
            transform.DOMove(cameraInstructions[_currentPos].pos, cameraInstructions[_currentPos].duration).SetEase(Ease.Linear);
        }
        
    }
}

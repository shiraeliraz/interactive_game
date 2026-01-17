using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BackgroundGlideForTruck : MonoBehaviour
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
        GameEvents.StartDriving += GlideBackground;
    }

    private void OnDisable()
    {
        GameEvents.StartDriving -= GlideBackground;
    }
    private void GlideBackground()
    {
        _currentPos++;
        if (_currentPos < cameraInstructions.Count)
        {
            transform.DOMove(cameraInstructions[_currentPos].pos, cameraInstructions[_currentPos].duration).OnComplete(StopDriving);
        }
        GameEvents.CameraGlide?.Invoke();
    }

    private void StopDriving()
    {
        GameEvents.StopDriving?.Invoke();
    }
}
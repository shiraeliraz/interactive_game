using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TomatoColor : MonoBehaviour
{
    [SerializeField] private List<Color> _colors;
    private int _currentPhase = 0;
    
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GameEvents.SingleRotPhase += OnRotPhase;
    }

    private void OnDisable()
    {
        GameEvents.SingleRotPhase -= OnRotPhase;
    }


    private void OnRotPhase()
    {
        Debug.Log("changing colors");
        if (_currentPhase >= _colors.Count)
        {
            return;
        }
        _spriteRenderer.DOColor(_colors[_currentPhase], 0.5f);
        _currentPhase++;
    }
}

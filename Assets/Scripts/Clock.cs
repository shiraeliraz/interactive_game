using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clock : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int timeForRot = 5;
    private Animator _animator;
    private bool _isPlaying;
    
    private void Update()
    {
        bool pressed =
            Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame
            ||
            Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        if (!pressed)
            return;
        Vector2 screenPos =
            Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame
                ? Mouse.current.position.ReadValue()
                : Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            GameEvents.TimePassed?.Invoke();
        }
    }

    private void OnEnable()
    {
        GameEvents.TimePassed +=  OnTimePassed;
        audioSource.enabled = false;
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void OnDisable()
    {
        GameEvents.TimePassed -= OnTimePassed;
    }

    private void OnTimePassed()
    {
        if (_isPlaying)
        {
            return;
        }
        _isPlaying = true;
        _animator.enabled = true;
        audioSource.enabled = true;
    }

    public void StopAudio()
    {
        audioSource.enabled = false;
        _animator.enabled = false;
        _isPlaying = false;
    }
}

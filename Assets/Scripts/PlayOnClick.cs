using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayOnClick : MonoBehaviour
{
    [SerializeField] private List<string> animationNames;
    [SerializeField] private Collider2D colliderToEnable;
    private int _currentAnimation = 0;
    private Animator _animator;
    private bool _isPlaying;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

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
            if (_isPlaying)
            {
                return;
            }

            if (_currentAnimation >= animationNames.Count)
            {
                return;
            }
            _animator.enabled = true;
            _animator.Play(animationNames[_currentAnimation]);
            _isPlaying = true;
        }
    }

    public void CarryOn()
    {
        _currentAnimation++;
        _isPlaying = false;
    }

    public void EnableTheirCollider()
    {
        colliderToEnable.enabled = true;
    }
}
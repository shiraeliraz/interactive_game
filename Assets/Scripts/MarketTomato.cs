using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarketTomato : MonoBehaviour
{
    [Header("Market Settings")]
    [SerializeField] private Vector3 marketTarget;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float duration = 1f;

    private bool _atMarket = false;
    private Tomato _tomatoScript;
    private void Update()
    {

        bool pressed =
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);

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
            Jump();
        }
    }

    private void Jump()
    {
        transform.DOJump(marketTarget, jumpPower, 1, duration)
            .SetUpdate(true).OnComplete(FinishedJump);
        transform.DORotate(Vector3.zero, 0.25f)
            .SetEase(Ease.Linear);
    }

    private void FinishedJump()
    {
        GameEvents.TomatoReachedMarket?.Invoke();
    }
}

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
    
    [Header("Bag Settings")]
    [SerializeField] private GameObject bag;
    [SerializeField] private float jumpPower2 = 2f;
    [SerializeField] private float duration2 = 1f;


    private bool _atCrate = true;
    private bool _canBeBought = false;
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
            if (_atCrate)
            {
                JumpToMarket();
            }
            else
            {
                if (_canBeBought)
                {
                    JumpToBag();
                }
            }
            
        }
    }

    private void OnEnable()
    {
        GameEvents.AllTomatoesInMarket += EnableBuy;
        GameEvents.TomatoInBag += DisableBuy;
    }

    private void OnDisable()
    {
        GameEvents.AllTomatoesInMarket -= EnableBuy;
        GameEvents.TomatoInBag -= DisableBuy;
    }

    private void JumpToMarket()
    {
        _atCrate = false;
        transform.DOJump(marketTarget, jumpPower, 1, duration)
            .SetUpdate(true).OnComplete(FinishedJumpToMarket);
        transform.DORotate(Vector3.zero, 0.25f)
            .SetEase(Ease.Linear);
    }
    private void JumpToBag()
    {
        _atCrate = false;
        transform.parent = bag.transform;
        transform.DOJump(bag.transform.position, jumpPower2, 1, duration2)
            .SetUpdate(true).OnComplete(FinishedJumpToBag);
    }

    private void FinishedJumpToMarket()
    {
        GameEvents.TomatoReachedMarket?.Invoke();
    }

    private void FinishedJumpToBag()
    {
        GameEvents.TomatoInBag?.Invoke();
    }

    private void EnableBuy()
    {
        _canBeBought = true;
    }

    private void DisableBuy()
    {
        _canBeBought = false;
    }
}

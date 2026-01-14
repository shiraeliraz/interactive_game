using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Tomato : MonoBehaviour
{
    [SerializeField] private Vector3 crateTarget;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float duration = 0.6f;
    [SerializeField] private GameObject crate;

    private void Jump()
    {
        Transform originalParent = transform.parent;
        transform.SetParent(null);

        transform.DOJump(crateTarget, jumpPower, 1, duration)
            .OnComplete(ParentCrate);
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
            Jump();
        }
    }

    private void ParentCrate()
    {
        transform.SetParent(crate.transform);
    }

}
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Tomato : MonoBehaviour
{
    [SerializeField] private Vector3 crateTarget;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float duration = 0.6f;
    [SerializeField] private GameObject crate;
    private bool _inCrate = false;
    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        GameEvents.CrateReachedTruck += OnCrateReachedTruck;
    }

    private void OnDisable()
    {
        GameEvents.CrateReachedTruck -= OnCrateReachedTruck;
    }
    // private void Start()
    // {
    //     _rigidbody = GetComponent<Rigidbody2D>();
    // }

    private void Jump()
    {
        Transform originalParent = transform.parent;
        transform.SetParent(null);

        transform.DOJump(crateTarget, jumpPower, 1, duration)
            .OnComplete(ParentCrate);
    }

    
    private void Update()
    {
        if (!_inCrate)
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
        
    }

    private void ParentCrate()
    {
        transform.SetParent(crate.transform);
        GameEvents.TomatoInBasket?.Invoke();
    }

    private void OnCrateReachedTruck()
    {
        _inCrate = true;
        // _rigidbody.gravityScale = 1;
    }

}
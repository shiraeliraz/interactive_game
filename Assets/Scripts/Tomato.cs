using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Tomato : MonoBehaviour
{
    [SerializeField] private Vector3 crateTarget;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float duration = 0.6f;
    [SerializeField] private GameObject crate;
    [SerializeField] private GameObject tomatoToActivate;
    private bool _inCrate = false;
    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        GameEvents.CrateReachedTruck += OnCrateReachedTruck;
        GameEvents.StopDriving += ParentCrate;
        GameEvents.StopDriving += OnTruckStop;
        GameEvents.CrateReachedFloor += OnCrateReachedFloor;
    }

    private void OnDisable()
    {
        GameEvents.CrateReachedTruck -= OnCrateReachedTruck;
        GameEvents.StopDriving -= ParentCrate;
        GameEvents.StopDriving -= OnTruckStop;
        GameEvents.CrateReachedFloor -= OnCrateReachedFloor;
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
    }

    private void Jump()
    {
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

    private void OnTruckStop()
    {
        _rigidbody.simulated = false;
        transform.SetParent(crate.transform);
        float newHeight = transform.position.y + 0.5f;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }

    private void OnCrateReachedTruck()
    {
        _inCrate = true;
        _rigidbody.gravityScale = 1;
        transform.SetParent(null);
    }

    private void OnCrateReachedFloor()
    {
        tomatoToActivate.SetActive(true);
        transform.SetParent(null);
        tomatoToActivate.transform.position = transform.position;
        tomatoToActivate.transform.rotation = transform.rotation;
        SpriteRenderer newTomatoRenderer = tomatoToActivate.GetComponent<SpriteRenderer>();
        SpriteRenderer oldTomatoRenderer = GetComponent<SpriteRenderer>();
        newTomatoRenderer.sprite = oldTomatoRenderer.sprite;
        gameObject.SetActive(false);
        
    }

}
using UnityEngine;
using UnityEngine.InputSystem;

public class InfiniteClicks : MonoBehaviour
{
    private bool _isPlaying;
    private Animator _animator;
    [SerializeField] private string triggerName;

    private void Start()
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

            _isPlaying = true;
            _animator.enabled = true;
            //_animator.SetTrigger(triggerName);
            
        }
    }

    public void FinishedPlaying()
    {
        _animator.enabled = false;
        _isPlaying = false;
    }
}

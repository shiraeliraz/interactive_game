using UnityEngine;
using UnityEngine.InputSystem;

public class PlayOnClick : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string animationName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
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
            animator.enabled = true;
            animator.Play(animationName);
        }
    }
}
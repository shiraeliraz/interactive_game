using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sink : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Vector3 dropInstantiationLocation = new Vector3(82.449f, 0.678f,0);
    [SerializeField] private Vector3 dropEndPos = new Vector3(82.449f, -0.515f,0);
    [SerializeField] private float dropLifeSpan = 0.2f;
    
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
            Dropdrop();
        }
    }

    private void Dropdrop()
    {
        var drop = Instantiate(dropPrefab, dropInstantiationLocation, Quaternion.identity);
        Transform dropTransform = drop.transform;

        dropTransform.DOMove(dropEndPos, dropLifeSpan)
            .OnComplete(() => KillDrop(drop));
    }

    private void KillDrop(GameObject drop)
    {
        Destroy(drop);
    }
    
}

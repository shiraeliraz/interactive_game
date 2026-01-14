using System;
using UnityEngine;

public class SeedSwallow : MonoBehaviour
{
    [SerializeField] private GameObject seed;

    private void Start()
    {
        Collider2D hillCollider = GetComponent<Collider2D>();
        hillCollider.enabled = false;
    }

    public void OnSwallow()
    {
        Destroy(seed);
    }
}

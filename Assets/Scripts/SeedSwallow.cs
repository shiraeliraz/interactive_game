using System;
using UnityEngine;

public class SeedSwallow : MonoBehaviour
{
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject tomatoes;
    [SerializeField] private GameObject crate;

    private void Start()
    {
        tomatoes.SetActive(false);
        Collider2D hillCollider = GetComponent<Collider2D>();
        hillCollider.enabled = false;
        crate.SetActive(false);
    }

    public void OnSwallow()
    {
        Destroy(seed);
    }

    public void ColorTomatoes()
    {
        tomatoes.SetActive(true);
        crate.SetActive(true);
        gameObject.SetActive(false);
    }
}

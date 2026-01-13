using UnityEngine;

public class SeedSwallow : MonoBehaviour
{
    [SerializeField] private GameObject seed;
    public void OnSwallow()
    {
        Destroy(seed);
    }
}

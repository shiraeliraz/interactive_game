using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip landSound;

    public void PlayLandSound()
    {
        audioSource.PlayOneShot(landSound);
    }
}

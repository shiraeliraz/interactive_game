using System.Collections;
using UnityEngine;

public class TrashRoutine : MonoBehaviour
{
    [SerializeField] private GameObject trashCan;
    private Animator _animator;



    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        trashCan.SetActive(false);
        GameEvents.TomatoIsRotten += OnTomatoRotten;

    }

    private void OnDisable()
    {
        GameEvents.TomatoIsRotten -= OnTomatoRotten;
    }

    private void OnTomatoRotten()
    {
        _animator.enabled = true;

    }

    public void TurnOnTrash()
    {
        trashCan.SetActive(true);
        StartCoroutine(Delay());

    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(8f);
        GameEvents.CameraGlide?.Invoke();
    }
}
 
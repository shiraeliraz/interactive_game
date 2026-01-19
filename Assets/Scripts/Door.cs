using UnityEngine;

public class Door : MonoBehaviour
{
    public void DoorOpened()
    {
        GameEvents.DoorOpened?.Invoke();
        GameEvents.CameraGlide?.Invoke();
    }
}

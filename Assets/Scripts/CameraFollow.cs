using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;

    void Update()
    {
        transform.LookAt(targetObject);
    }
}
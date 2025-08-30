
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Vector3 offset = new Vector3(0, 4, -10);
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 currentVelocity = Vector3.zero;

    [SerializeField] private Transform target;
    private Vector3 targetPosition = Vector3.zero;


    
    void Update()
    {
        targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}

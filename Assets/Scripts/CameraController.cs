using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{

    [Header("Target")]
    [SerializeField] private Transform target; // The player to follow

    [Header("Settings")]
    [SerializeField] private Vector2 deadZoneSize; // The size of the window where the camera doesn't move
    [SerializeField] private float smoothSpeed = 5f; // How fast the camera follows

    private Vector3 targetPanPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the boundaries of the dead zone based on the camera's current position
        float minX = transform.position.x - deadZoneSize.x / 2;
        float maxX = transform.position.x + deadZoneSize.x / 2;
        float minY = transform.position.y - (deadZoneSize.y / 6);
        float maxY = transform.position.y + (deadZoneSize.y / 6)*5;

        targetPanPosition = transform.position;
        if (target.position.x < minX)
        {
            targetPanPosition.x = target.position.x+deadZoneSize.x/2 ;
        }
        else if (target.position.x > maxX)
        {
            targetPanPosition.x = target.position.x - deadZoneSize.x/2;
        }


        if (target.position.y < minY)
        {
            targetPanPosition.y = target.position.y + deadZoneSize.y/2;
        }
        else if (target.position.y > maxY)
        {
            targetPanPosition.y = target.position.y - deadZoneSize.y/2 ;
        }


        transform.position = Vector3.Lerp(transform.position, targetPanPosition, smoothSpeed * Time.deltaTime);
    }
}

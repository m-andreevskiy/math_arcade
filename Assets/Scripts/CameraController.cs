using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{

    [Header("Target")]
    [SerializeField] private Transform target; // The player to follow

    [Header("Settings")]
    [SerializeField] private Vector2 deadZoneSize; // The size of the window where the camera doesn't move
    [SerializeField] private float smoothSpeed = 5f;
 // How fast the camera follows

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

        Vector3 targetDestination = transform.position;

        // Check if the player is outside the dead zone bounds.
        bool isOutsideDeadZone = (
            target.position.x < minX ||
            target.position.x > maxX ||
            target.position.y < minY ||
            target.position.y > maxY
        );

        // If the player IS outside the dead zone...
        if (isOutsideDeadZone)
        {
            // ...then the target destination becomes the player's position (plus any offset).
            // This will make the camera pan to re-center the player.
            targetDestination = new Vector3(target.position.x, target.position.y, transform.position.z);
        }


        transform.position = Vector3.Lerp(transform.position, targetDestination, smoothSpeed * Time.deltaTime);
    }
}

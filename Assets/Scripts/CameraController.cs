using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    public float minBoundsX, maxBoundsX, minBoundsZ, maxBoundsZ;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBoundsX, maxBoundsX);
            targetPosition.z = Mathf.Clamp(targetPosition.z, minBoundsZ, maxBoundsZ);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            cam.orthographicSize = Mathf.Max((maxBoundsZ - minBoundsZ) / 2f, (maxBoundsX - minBoundsX) / (2f * cam.aspect));
        }
    }
}
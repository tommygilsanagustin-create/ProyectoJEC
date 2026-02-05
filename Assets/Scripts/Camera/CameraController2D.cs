using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public Transform target;

    [Header("Follow")]
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    [Header("Offset")]
    public Vector2 offset;

    [Header("Limits")]
    public Vector2 minPosition;
    public Vector2 maxPosition;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        smoothPosition.x = Mathf.Clamp(smoothPosition.x, minPosition.x, maxPosition.x);
        smoothPosition.y = Mathf.Clamp(smoothPosition.y, minPosition.y, maxPosition.y);

        transform.position = smoothPosition;
    }
}

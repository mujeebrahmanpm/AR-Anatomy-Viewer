using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ModelGestureController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera arCamera;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float panSpeed = 0.002f;
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 3f;

    [Header("Reset values (local to parent)")]
    [SerializeField] private Vector3 resetPosition = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 resetEulerAngles = new Vector3(0f, 180f, 0f);
    [SerializeField] private Vector3 resetScale = new Vector3(1f, 1f, 1f);

    private void OnEnable() => EnhancedTouchSupport.Enable();
    private void OnDisable() => EnhancedTouchSupport.Disable();

    void Start()
    {
        if (arCamera == null) arCamera = Camera.main;
    }

    void Update()
    {
        if (target == null) return;

        var touches = Touch.activeTouches;

        if (touches.Count == 3)
        {
            HandleRotate(touches[0]);
        }
        else if (touches.Count == 2)
        {
            HandlePinchAndPan(touches[0], touches[1]);
        }
    }

    public void ResetTransform()
    {
        target.localPosition = resetPosition;
        target.localEulerAngles = resetEulerAngles;
        target.localScale = resetScale;
    }

    void HandleRotate(Touch touch)
    {
        Vector2 delta = touch.delta;
        target.Rotate(Vector3.up, -delta.x * rotationSpeed, Space.World);
        target.Rotate(Vector3.right, delta.y * rotationSpeed, Space.World);
    }

    void HandlePinchAndPan(Touch touchA, Touch touchB)
    {
        Vector2 prevA = touchA.screenPosition - touchA.delta;
        Vector2 prevB = touchB.screenPosition - touchB.delta;

        float prevDistance = Vector2.Distance(prevA, prevB);
        float currentDistance = Vector2.Distance(touchA.screenPosition, touchB.screenPosition);

        if (prevDistance > 0.01f)
        {
            float scaleFactor = currentDistance / prevDistance;
            float newScale = Mathf.Clamp(target.localScale.x * scaleFactor, minScale, maxScale);
            target.localScale = Vector3.one * newScale;
        }

        Vector2 avgDelta = (touchA.delta + touchB.delta) * 0.5f;

        Vector3 camRight = arCamera != null ? arCamera.transform.right : Vector3.right;
        Vector3 camUp = arCamera != null ? arCamera.transform.up : Vector3.up;

        Vector3 pan = (camRight * avgDelta.x + camUp * avgDelta.y) * panSpeed;
        target.position += pan;
    }
}
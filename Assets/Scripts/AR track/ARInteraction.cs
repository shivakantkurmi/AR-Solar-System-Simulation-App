using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ARInteraction : MonoBehaviour
{
    private float rotationSpeed = 0.2f; // Adjust rotation speed
    private float scaleSpeed = 0.0001f; // Slower scaling effect
    private float minScale = 0.1f; // Minimum scale limit
    private float maxScale = 2.0f; // Maximum scale limit

    void Start()
    {
        EnhancedTouchSupport.Enable(); // Enable Enhanced Touch System
    }

    void Update()
    {
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;

        if (activeTouches.Count == 1) // Single-finger rotation
        {
            var touch = activeTouches[0];

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                float rotateY = touch.delta.x * rotationSpeed;
                transform.Rotate(0, -rotateY, 0, Space.World);
            }
        }
        else if (activeTouches.Count == 2) // Two-finger pinch to scale
        {
            var touch1 = activeTouches[0];
            var touch2 = activeTouches[1];

            if (touch1.phase == UnityEngine.InputSystem.TouchPhase.Moved &&
                touch2.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                float prevDistance = (touch1.startScreenPosition - touch2.startScreenPosition).magnitude;
                float currentDistance = (touch1.screenPosition - touch2.screenPosition).magnitude;
                float scaleChange = (currentDistance - prevDistance) * scaleSpeed;

                // Relative scaling using the object's current scale
                Vector3 currentScale = transform.localScale;
                Vector3 scaleFactor = new Vector3(scaleChange, scaleChange, scaleChange);

                // Apply relative scaling to the object
                Vector3 newScale = currentScale + scaleFactor;

                // Ensure scale doesn't go below the minimum or above the maximum
                newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
                newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
                newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

                transform.localScale = newScale;
            }
        }
    }
}

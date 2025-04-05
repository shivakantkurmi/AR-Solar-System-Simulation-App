using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class ObjectControl : MonoBehaviour
{
    public Transform augmentedObject;  // Reference to the augmented object
    public float rotateSpeed = 5f;
    public float scaleSpeed = 0.02f;  // Scale speed multiplier
    private float minScale = 0.1f;    // Minimum scale limit
    private float maxScale = 2f;      // Maximum scale limit

    private float lastPinchDistance = 0;

    void Start()
    {
        EnhancedTouchSupport.Enable(); // ✅ Enables touch in Input System
    }

    void Update()
    {
        HandleTouchInput();  // 📱 Handle Touch Controls
    }

    // 📱 **Handles Touch Input Based on Screen Side**
    private void HandleTouchInput()
    {
        if (Touchscreen.current == null) return;

        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        int touchCount = activeTouches.Count;

        if (touchCount == 1) // Single-finger swipe for rotation
        {
            UnityEngine.InputSystem.EnhancedTouch.Touch touch = activeTouches[0];
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                float screenMid = Screen.width / 2;
                
                if (touch.screenPosition.x >= screenMid) // Right Side → Rotate
                {
                    augmentedObject.Rotate(Vector3.forward, touch.delta.x*0.05f * rotateSpeed * Time.deltaTime);
                }
            }
        }

        if (touchCount == 2) // Two-finger pinch for scaling
        {
            Vector2 touch1 = activeTouches[0].screenPosition;
            Vector2 touch2 = activeTouches[1].screenPosition;
            float pinchDistance = Vector2.Distance(touch1, touch2);

            if (lastPinchDistance != 0)
            {
                float delta = pinchDistance - lastPinchDistance;
                float scaleFactor = 1 + delta * scaleSpeed * 0.01f;

                // Calculate new scale
                Vector3 newScale = augmentedObject.localScale * scaleFactor;
                
                // Clamp scale to stay within the allowed range
                newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
                newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
                newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);
                
                augmentedObject.localScale = newScale;
            }
            lastPinchDistance = pinchDistance;
        }
        else
        {
            lastPinchDistance = 0;
        }
    }
}

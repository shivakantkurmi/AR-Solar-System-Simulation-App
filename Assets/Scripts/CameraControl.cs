
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float rotateSpeed = 5f;
    public float zoomSpeed = 100f;
    public float panSpeed = 60f;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector2 zoomInput;
    private bool isRotating = false;
    private bool isPanning = false;
    private bool isZooming = false;
    private float lastPinchDistance = 0;

    void Start()
    {
        EnhancedTouchSupport.Enable(); // ✅ Enables touch in Input System
    }

    void Update()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
        HandlePCControls();  // 🎮 Handle Mouse & Keyboard
        #endif
        
        HandleTouchInput();  // 📱 Handle Touch Controls
    }

    // ✅ **Handles PC Movement, Zoom, Rotation, and Panning**
    private void HandlePCControls()
    {
        HandleMovement();
        HandlePCZoom();
        HandlePCRotation();
        HandlePCPanning();
    }

    private void HandleMovement()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
            moveDirection.y = 0;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void HandlePCZoom()
    {
        if (!isZooming) // Prevent interference with touch zoom
        {
            transform.Translate(Vector3.forward * zoomInput.y * zoomSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void HandlePCRotation()
    {
        if (isRotating && lookInput != Vector2.zero)
        {
            transform.Rotate(Vector3.up, lookInput.x * rotateSpeed * Time.deltaTime);
        }
    }

    private void HandlePCPanning()
    {
        if (isPanning && lookInput != Vector2.zero)
        {
            transform.Translate(new Vector3(-lookInput.x, -lookInput.y, 0) * panSpeed * Time.deltaTime, Space.Self);
        }
    }

    // 📱 **Handles Touch Input Based on Screen Side**
    private void HandleTouchInput()
    {
        if (Touchscreen.current == null) return;

        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        int touchCount = activeTouches.Count;

        if (touchCount == 1) // Single-finger swipe for pan or rotate based on screen position
        {
            UnityEngine.InputSystem.EnhancedTouch.Touch touch = activeTouches[0];

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                float screenMid = Screen.width / 2;

                if (touch.screenPosition.x < screenMid) // Left Side of Screen → Pan
                {
                    Vector2 touchDelta = touch.delta;
                    transform.Translate(new Vector3(-touchDelta.x, -touchDelta.y, 0) * panSpeed * Time.deltaTime, Space.Self);
                }
                else // Right Side of Screen → Rotate
                {
                    transform.Rotate(Vector3.up, touch.delta.x*0.5f * rotateSpeed * Time.deltaTime);
                }
            }
        }

        if (touchCount == 2) // Two-finger pinch for zoom
        {
            Vector2 touch1 = activeTouches[0].screenPosition;
            Vector2 touch2 = activeTouches[1].screenPosition;
            float pinchDistance = Vector2.Distance(touch1, touch2);

            if (lastPinchDistance != 0)
            {
                float delta = pinchDistance - lastPinchDistance;
                transform.Translate(Vector3.forward * delta * zoomSpeed * 0.01f, Space.Self);
            }
            lastPinchDistance = pinchDistance;
        }
        else
        {
            lastPinchDistance = 0;
        }
    }

    // 🎮 **PC Input System Callbacks**
    public void OnMove(InputAction.CallbackContext context) { moveInput = context.ReadValue<Vector2>(); }
    public void OnLook(InputAction.CallbackContext context) { lookInput = context.ReadValue<Vector2>(); }
    public void OnZoom(InputAction.CallbackContext context) { zoomInput = context.ReadValue<Vector2>(); }

    public void OnRotate(InputAction.CallbackContext context) 
    { 
        if (context.started) isRotating = true;
        else if (context.canceled) isRotating = false;
    }

    public void OnPan(InputAction.CallbackContext context) 
    { 
        if (context.started) isPanning = true;
        else if (context.canceled) isPanning = false;
    }
}

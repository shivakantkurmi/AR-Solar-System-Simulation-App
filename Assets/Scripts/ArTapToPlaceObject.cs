using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ARTapToPlace : MonoBehaviour
{
    public GameObject objectToPlace;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedObject;
    private Rigidbody rb;
    private float planeHeight = 0f;  // Stores the AR plane height

    private void Start()
    {
        arRaycastManager = FindFirstObjectByType<ARRaycastManager>();
    }

    private void Update()
    {
        Vector2 touchPosition = Vector2.zero;

        // Check for touch input (mobile)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // Check for mouse click (PC)
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            touchPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return; // No input detected
        }

        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                // Spawn the ball
                spawnedObject = Instantiate(objectToPlace, hitPose.position, Quaternion.identity);
                
                // Add Rigidbody for gravity
                rb = spawnedObject.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = spawnedObject.AddComponent<Rigidbody>();
                    rb.useGravity = true;
                    rb.mass = 1f;
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                    rb.interpolation = RigidbodyInterpolation.Interpolate;
                }

                // Store the AR plane height
                planeHeight = hitPose.position.y;
            }
            else
            {
                // Move the ball to the new position
                Vector3 newPosition = hitPose.position;
                newPosition.y = planeHeight; // Keep it on the AR plane

                spawnedObject.transform.position = newPosition;
                rb.linearVelocity = Vector3.zero; // Stop any existing motion
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}

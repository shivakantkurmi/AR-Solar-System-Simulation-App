using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    public Camera mainCamera; // Reference to the AR camera

    void Update()
    {
        if (mainCamera != null)
        {
            // Make the UI element face the camera by rotating it
            transform.LookAt(mainCamera.transform);

            // To prevent flipping, adjust the rotation by setting the forward direction correctly
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = 0f;  // Prevent tilt in the X axis (optional)
            targetRotation.z = 0f;  // Prevent tilt in the Z axis (optional)
            
            // Flip the UI by rotating it 180 degrees on the Y-axis if necessary
            if (Vector3.Dot(mainCamera.transform.forward, transform.forward) < 0)
            {
                targetRotation.y += 180f; // Rotate the canvas 180 degrees around the Y-axis
            }

            transform.eulerAngles = targetRotation;
        }
    }
}

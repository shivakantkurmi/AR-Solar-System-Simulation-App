using UnityEngine;

public class LockParticleYAxis : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial rotation when the object is created
        initialRotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y, 0);
    }

    void LateUpdate()
    {
        // Maintain only Y-axis rotation, ignoring any tilt due to AR marker rotation
        transform.rotation = initialRotation;
    }
}

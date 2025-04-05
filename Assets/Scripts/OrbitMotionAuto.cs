using UnityEngine;

public class OrbitMotionAuto : MonoBehaviour
{
    public Transform centerObject; // The object this will orbit (e.g., Sun for planets)
    public float orbitSpeed = 10f; // Speed of orbit
    public Vector3 orbitAxis = Vector3.up; // Orbit around which axis (default: Y-axis)

    public float rotationSpeed = 20f; // Speed of self-rotation
    public Vector3 rotationAxis = Vector3.up; // Axis for self-rotation

    private float orbitRadius; // Distance from the center object
    private float angle = 0f; // Current orbit angle

    void Start()
    {
        if (centerObject == null)
        {
            Debug.LogError(gameObject.name + " has no center object assigned!");
            return;
        }

        // Calculate the initial orbit radius from the starting position
        orbitRadius = Vector3.Distance(transform.position, centerObject.position);
        
        // Calculate the initial angle based on the current position relative to the center object
        Vector3 direction = (transform.position - centerObject.position).normalized;
        angle = Mathf.Atan2(direction.z, direction.x);  // Convert position to angle
    }

    void Update()
    {
        if (centerObject == null) return;

        // Dynamically update the orbit radius if scale changes
        orbitRadius = Vector3.Distance(transform.position, centerObject.position);

        // Update the angle to make the object move along its orbit
        angle += orbitSpeed * Time.deltaTime;

        // Calculate the new position in the orbit, considering the updated orbit radius
        float x = centerObject.position.x + Mathf.Cos(angle) * orbitRadius;
        float z = centerObject.position.z + Mathf.Sin(angle) * orbitRadius;

        // Apply the new position while keeping the Y-axis unchanged
        transform.position = new Vector3(x, transform.position.y, z);

        // Rotate the planet around its own axis
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);

        // Optional: Make the planet face its orbit center (uncomment if needed)
        // transform.LookAt(centerObject);
    }
}

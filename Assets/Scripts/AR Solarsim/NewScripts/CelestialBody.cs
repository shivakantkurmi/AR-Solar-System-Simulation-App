using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform orbitCenter; // The reference object (e.g., Sun)
    public float orbitalPeriodDays = 365.25f;

    [Header("Rotation Settings")]
    public float rotationPeriodHours = 24f;

    private float orbitRadius;
    private float orbitAngle;
    private float rotationAngle;
    private Vector3 initialOffset; // Stores the initial relative position

    void Start()
    {
        if (orbitCenter != null)
        {
            // Store initial offset from the Sun
            initialOffset = transform.position - orbitCenter.position;
            orbitRadius = initialOffset.magnitude;

            // Get initial orbit angle
            Vector3 direction = initialOffset.normalized;
            orbitAngle = Mathf.Atan2(direction.z, direction.x);
        }

        // Set initial self-rotation
        rotationAngle = transform.eulerAngles.y;
    }

    void Update()
    {
        if (GlobalSpeedController.Instance == null || orbitCenter == null)
            return;

        // --- REVOLUTION (Orbit Motion) ---
        float simOrbitalPeriod = (orbitalPeriodDays / 365.25f) * GlobalSpeedController.Instance.earthYearSimTime;
        float orbitAngularSpeed = (2f * Mathf.PI) / simOrbitalPeriod;
        orbitAngle += orbitAngularSpeed * Time.deltaTime;

        // Compute new orbit position
        float x = Mathf.Cos(orbitAngle) * orbitRadius;
        float z = Mathf.Sin(orbitAngle) * orbitRadius;
        transform.position = orbitCenter.position + new Vector3(x, 0, z);

        // --- ROTATION (Self-Spin) ---
        float simRotationPeriod = (rotationPeriodHours / 24f) * GlobalSpeedController.Instance.earthDaySimTime;
        float rotationAngularSpeed = 360f / simRotationPeriod;
        rotationAngle += rotationAngularSpeed * Time.deltaTime;

        // Rotate only around Y-axis (self-rotation)
        transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
    }

    // ✅ FUNCTION TO UPDATE ORBIT RADIUS (Called by OrbitScaler)
    public void UpdateOrbitRadius(float scaleFactor)
    {
        orbitRadius = initialOffset.magnitude * scaleFactor;
    }
}

using UnityEngine;

public class OrbitScaler : MonoBehaviour
{
    public Transform referenceObject; // The object that controls the scale (e.g., Sun)
    public CelestialBody[] planets;   // List of planets affected by scaling

    private float initialScale;

    void Start()
    {
        if (referenceObject == null || planets.Length == 0)
        {
            Debug.LogError("OrbitScaler: Reference object or planets missing!");
            return;
        }

        initialScale = referenceObject.localScale.x; // Store original scale
    }

    void Update()
    {
        if (referenceObject == null) return;

        float scaleFactor = referenceObject.localScale.x / initialScale; // Scale ratio

        foreach (CelestialBody planet in planets)
        {
            if (planet != null)
                planet.UpdateOrbitRadius(scaleFactor);
        }
    }
}

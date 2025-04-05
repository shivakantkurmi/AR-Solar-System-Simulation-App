using UnityEngine;
using Vuforia;

public class OrbitPathAuto : MonoBehaviour
{
    public Transform centerObject; // The object the planet orbits (e.g., the Sun)
    public int segments = 100;
    private float lineWidth = 0.05f;
    private LineRenderer lineRenderer;
    private ObserverBehaviour observerBehaviour; 

    void Start()
    {
        observerBehaviour = GetComponentInParent<ObserverBehaviour>();

        if (observerBehaviour == null || centerObject == null)
        {
            Debug.LogError("Tracking not found or center object missing!");
            return;
        }

        // Setup LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(1, 1, 1, 0.3f);
        lineRenderer.endColor = new Color(1, 1, 1, 0f);

        lineRenderer.enabled = false; // Hide initially

        // Subscribe to tracking events
        observerBehaviour.OnTargetStatusChanged += OnTrackingChanged;
    }

    void LateUpdate()
    {
        if (centerObject == null || !lineRenderer.enabled) return;

        // ✅ Always update the orbit radius dynamically
        float orbitRadius = Vector3.Distance(transform.position, centerObject.position);

        DrawOrbit(orbitRadius);
    }

    void DrawOrbit(float orbitRadius)
    {
        for (int i = 0; i <= segments; i++)
        {
            float angle = (i / (float)segments) * 2 * Mathf.PI;
            float x = centerObject.position.x + Mathf.Cos(angle) * orbitRadius;
            float z = centerObject.position.z + Mathf.Sin(angle) * orbitRadius;
            lineRenderer.SetPosition(i, new Vector3(x, centerObject.position.y, z)); // ✅ Keeps orbit level with Sun
        }
    }

    void OnTrackingChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            lineRenderer.enabled = true; // Show orbit when tracked
        }
        else
        {
            lineRenderer.enabled = false; // Hide orbit when lost
        }
    }
}

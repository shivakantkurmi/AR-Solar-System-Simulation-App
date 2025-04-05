using UnityEngine;

public class ARTargetHandler : MonoBehaviour
{
    private Transform markerTransform; // Reference to the marker (Image Target)

    void Start()
    {
        markerTransform = transform.parent; // Assuming the model is a child of the marker
    }

    public void OnTargetFound()
    {
        // Reset the object's transform to match the marker's transform
        transform.position = markerTransform.position;
        transform.rotation = markerTransform.rotation;
        transform.localScale = markerTransform.localScale;

        gameObject.SetActive(true); // Show the object
    }

    public void OnTargetLost()
    {
        gameObject.SetActive(false); // Hide the object when marker is lost
    }
}

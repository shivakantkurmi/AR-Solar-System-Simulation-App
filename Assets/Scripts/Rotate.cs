using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = -5f;
    public Vector3 direction = new Vector3(0, 0, 1);
    public NewTimeSlider newTimeSlider;
    // private Quaternion initialRotation;

    void Start()
    {
        // Store the initial rotation of the object
        // initialRotation = transform.rotation;
    }

    void Update()
    {
        // Ensure rotation is only around the defined axis and ignore parent's rotation
        float newSpeed = speed / newTimeSlider.speedSlider.value;
        // transform.rotation = initialRotation; // Reset to initial rotation before applying controlled rotation
        transform.Rotate(direction, newSpeed * Time.deltaTime, Space.Self);
    }
}

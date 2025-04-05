using UnityEngine;

public class FollowSaturn : MonoBehaviour
{
    public Transform saturn; // Assign Saturn in the Inspector

    void Update()
    {
        if (saturn != null)
        {
            transform.position = saturn.position; // Keep the rings at the same position as Saturn
        }
    }
}

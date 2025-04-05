using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    public float forceAmount = 5f; // Strength of hit
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Assign button actions dynamically
        GameObject.Find("LeftButton").GetComponent<Button>().onClick.AddListener(() => HitBall(Vector3.left));
        GameObject.Find("RightButton").GetComponent<Button>().onClick.AddListener(() => HitBall(Vector3.right));
    }
    

    void HitBall(Vector3 direction)
    {
        rb.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
}

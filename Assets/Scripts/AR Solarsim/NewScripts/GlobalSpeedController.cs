using UnityEngine;

public class GlobalSpeedController : MonoBehaviour
{
    public static GlobalSpeedController Instance { get; private set; }

    [Tooltip("Simulation time (in seconds) for one Earth revolution (1 Earth year). " +
             "For example, if set to 60, Earth completes an orbit in 60 simulation seconds.")]
    public float earthYearSimTime = 60f;

    // Derived simulation time for one Earth day.
    public float earthDaySimTime { get { return earthYearSimTime / 365.25f; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}

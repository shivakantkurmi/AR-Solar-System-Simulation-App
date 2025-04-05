using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewTimeSlider : MonoBehaviour
{
    [Tooltip("Slider to control the simulation time for one Earth year (in seconds).")]
    public Slider speedSlider;
    
    [Tooltip("TextMeshProUGUI to display the conversion ratio (1 real sec = X sim sec).")]
    public TextMeshProUGUI conversionRatioText;
    
    // [Tooltip("TextMeshProUGUI to display the simulation time elapsed in the simulation.")]
    // public TextMeshProUGUI simulationTimeText;
    
    [Tooltip("TextMeshProUGUI to display the equivalent real-world time.")]
    public TextMeshProUGUI realTimeText;

    private float realTimeMultiplier = 1f; // Multiplier for real-world time speed
    private float realTimeElapsed = 0f; // Tracks elapsed real-world time
    private float rTime=0f;
    private float rVal=0f;

    void Start()
    {
        if (GlobalSpeedController.Instance != null)
        {
            // Initialize slider value from the global controller.
            speedSlider.value = GlobalSpeedController.Instance.earthYearSimTime;
        }

        // Add listener to update when slider value changes.
        speedSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Update the conversion display immediately.
        UpdateSliderText(speedSlider.value);
    }

    void OnSliderValueChanged(float newVal)
    {
        if (GlobalSpeedController.Instance != null)
        {
            // Update the global simulation parameter (Earth year simulation time).
            GlobalSpeedController.Instance.earthYearSimTime = newVal;
        }
        UpdateSliderText(newVal);
    }

    void UpdateSliderText(float value)
    {
        if (conversionRatioText != null)
        {
            // Set the real-time multiplier to adjust the simulation time accordingly.
            realTimeMultiplier = 1f / value;  // E.g., when value = 60, 1 real second = 1/60 sim year.
            rTime=value;
            
            // Update the text showing the conversion of simulation time to real-world time.
            conversionRatioText.text = "1 Earth Year = " + value.ToString("F0") + " s";
        }
    }

    void Update()
    {
        if ( realTimeText != null && GlobalSpeedController.Instance != null)
        {
            // Track real-world time elapsed in terms of simulation time.
            realTimeElapsed += Time.deltaTime; // Track the real-world time in seconds.
            rVal+=Time.deltaTime/rTime;
            // Display the equivalent real-world time
            realTimeText.text = "Real Time: " + rVal.ToString("F2") + " Years";
        }
    }
}

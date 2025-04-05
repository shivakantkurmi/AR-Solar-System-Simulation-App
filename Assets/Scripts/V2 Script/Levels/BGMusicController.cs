using UnityEngine;
using UnityEngine.UI;

public class BGMusicController : MonoBehaviour
{
    public AudioSource bgMusic;  // Background music source
    public Slider timeSlider;    // Slider that controls the time (1 - 365)

    [Range(0f, 1f)] public float minVolume = 0.2f; // Minimum volume level
    [Range(0f, 1f)] public float maxVolume = 1f;   // Maximum volume level
    
    [Range(0.5f, 2f)] public float minPitch = 0.8f; // Minimum pitch level
    [Range(0.5f, 2f)] public float maxPitch = 1.5f; // Maximum pitch level

    void Start()
    {
        if (bgMusic == null)
        {
            Debug.LogError("No AudioSource assigned for background music!");
            return;
        }

        if (timeSlider == null)
        {
            Debug.LogError("No time slider assigned!");
            return;
        }

        bgMusic.loop = true;
        bgMusic.Play();

        // Set initial values based on the slider
        AdjustMusic(timeSlider.value);

        // Listen for slider value changes
        timeSlider.onValueChanged.AddListener(AdjustMusic);
    }

    void Update()
    {
        // Pause the music when the game is paused (Time.timeScale == 0)
        if (Time.timeScale == 0 && bgMusic.isPlaying)
        {
            bgMusic.Pause();
        }
        else if (Time.timeScale > 0 && !bgMusic.isPlaying)
        {
            bgMusic.UnPause();
        }
    }

    void AdjustMusic(float sliderValue)
    {
        if (bgMusic == null) return;

        // Normalize slider value to a range between 0 and 1 (inverted for volume)
        float normalizedValue = (sliderValue - 1) / (365 - 1);
        float invertedVolume = Mathf.Lerp(maxVolume, minVolume, normalizedValue);

        // Adjust volume and pitch based on the inverted value
        bgMusic.volume = invertedVolume;
        bgMusic.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedValue);
    }
}

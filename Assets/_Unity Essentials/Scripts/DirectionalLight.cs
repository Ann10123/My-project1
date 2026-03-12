using UnityEngine;

public class DirectionalLight : MonoBehaviour
{
    [Tooltip("How many seconds the bright day lasts")]
    public float dayDuration = 15f;

    [Tooltip("How many seconds the dark night lasts")]
    public float nightDuration = 5f;

    [Tooltip("Sun horizontal tilt angle (e.g., 45 degrees)")]
    public float sunTilt = 45f;

    [Tooltip("Light colors")]
    public Gradient lightColor;

    [Tooltip("Bird audio source")]
    public AudioSource birdSounds;

    [Tooltip("When the birds fall silent")]
    public float birdSleepAngle = 180f;

    private Light myLight;
    private float currentAngle = 0f; //Sun angle from 0 to 360

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        // Check if it's currently day (angle from 0 to 180 degrees)
        bool isDay = currentAngle < 180f;

        // Determine the speed depending on whether it's day or night
        float currentSpeed = isDay ? (180f / dayDuration) : (180f / nightDuration);

        //Move the sun
        currentAngle += currentSpeed * Time.deltaTime;

        if (currentAngle >= 360f)
        {
            currentAngle = 0f; //Start a new day
        }

        //Rotate the object
        transform.localRotation = Quaternion.Euler(currentAngle, sunTilt, 0f);

        // Change the color on objects (convert angles 0-360 to percentages 0-1)
        if (myLight != null)
        {
            myLight.color = lightColor.Evaluate(currentAngle / 360f);
        }

        // birds sounds
        if (birdSounds != null)
        {
            // day = 0.25f, night = 0f
            float targetVolume = (currentAngle < birdSleepAngle) ? 0.25f : 0f;

            // volume down
            birdSounds.volume = Mathf.MoveTowards(birdSounds.volume, targetVolume, Time.deltaTime * 0.25f);
        }
    }
}
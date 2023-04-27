using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    public Light sun; // Assign the directional light that acts as the sun in the inspector

    public float secondsInFullDay = 120f; // The number of seconds in a full day cycle
    [Range(0, 1)]
    public float currentTimeOfDay = 0; // The current time of day represented as a value between 0 and 1
    private float timeMultiplier = 1f; // Allows the time of day to be sped up or slowed down

    void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }

        GameObject[] streetlights = GameObject.FindGameObjectsWithTag("streetlights"); // find all the streetlights in the scene

        if (currentTimeOfDay >= 0.75f || currentTimeOfDay <= 0.25f) // Between 23:30 and 06:00
        {
            foreach (GameObject streetlight in streetlights)
            {
                streetlight.SetActive(true); // Enable the streetlights
            }
            // Debug.Log("Turn ON streetlights");
        }
        else
        {
            foreach (GameObject streetlight in streetlights)
            {
                streetlight.SetActive(false); // Disable the streetlights
            }
            // Debug.Log("Turn OFF streetlights");
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
    }
}
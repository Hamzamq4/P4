using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    public Light sun; // Assign the directional light that acts as the sun in the inspector

    public GameObject nightObject1; // Assign the first game object that should only appear at night
    public GameObject nightObject2; // Assign the second game object that should only appear at night

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

        if (currentTimeOfDay >= 0.75f || currentTimeOfDay <= 0.25f) // Between 22:30 and 06:00
        {
            nightObject1.SetActive(true); // Show the first night object
            nightObject2.SetActive(true); // Show the second night object
            Debug.Log("Turn ON lights");
        }
        else
        {
            nightObject1.SetActive(false); // Hide the first night object
            nightObject2.SetActive(false); // Hide the second night object
            Debug.Log("Turn OFF lights");
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
    }
}

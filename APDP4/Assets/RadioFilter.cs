using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioFilter : MonoBehaviour
{
    private AudioHighPassFilter highPassFilter;
    private AudioLowPassFilter lowPassFilter;
    private AudioDistortionFilter distortion;
    private AudioSource audioSource;

    [RangeAttribute(0, 20000)]
    public int lowFrequencyCutoff = 1000;

    [RangeAttribute(0, 20000)]
    public int highFrequencyCutoff = 5000;

    [RangeAttribute(0, 1)]
    public float distortionLevel = 0.6f;

    [RangeAttribute(0, 1)]
    public float volume = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        highPassFilter = gameObject.AddComponent<AudioHighPassFilter>();
        lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
        distortion = gameObject.AddComponent<AudioDistortionFilter>();

        highPassFilter.cutoffFrequency = lowFrequencyCutoff;
        lowPassFilter.cutoffFrequency = highFrequencyCutoff;
        distortion.distortionLevel = distortionLevel;
        audioSource.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

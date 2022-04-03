using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDetector : MonoBehaviour
{

    public const string SCREWDRIVER_TAG = "Screwdriver";
    public GameObject beepObject;
    private AudioSource beepAudioSource;

    public GameObject onObject;
    private AudioSource onAudioSource;
    
    public GameObject offObject;
    private AudioSource offAudioSource;

    public float maxVolume; // Highest volume for beeping
    public float maxDist; // Maximum distance (of closest screwdriver) to record sound
    void Start()
    {
        beepAudioSource = beepObject.GetComponent<AudioSource>();
        onAudioSource = onObject.GetComponent<AudioSource>();
        offAudioSource = offObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle on/off
        if(Input.GetKeyDown(KeyCode.X)){
            if(beepAudioSource.mute){
                beepAudioSource.mute = false;

                onAudioSource.Play();
            } else {
                beepAudioSource.mute = true;
                offAudioSource.Play();
            }
        }

        // Calculating frequency
        float minDist = maxDist;
        GameObject[] screwdrivers = GameObject.FindGameObjectsWithTag(SCREWDRIVER_TAG);
        foreach(GameObject s in screwdrivers){
            if(Vector3.Distance(s.transform.position, transform.position) < minDist){
                minDist = Vector3.Distance(s.transform.position, transform.position);
            }
        }

        float ratio = (maxDist - minDist) / maxDist;
        beepAudioSource.volume = (ratio * ratio) * maxVolume; // Square to not have full volume all the time
        
    }
}

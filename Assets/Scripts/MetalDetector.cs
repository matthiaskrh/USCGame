using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDetector : MonoBehaviour
{

    public const string SCREWDRIVER_TAG = "Screwdriver";
    public const string DOOR_TAG = "Door";

    public const string MONSTER_TAG = "Monster";
    public GameObject beepObject;
    private AudioSource beepAudioSource;

    public GameObject onObject;
    private AudioSource onAudioSource;
    
    public GameObject offObject;
    private AudioSource offAudioSource;

    public float forceOnDistance; // Distance for monster to be within to force ON of metal detector;

    public float maxVolume; // Highest volume for beeping
    public float maxDist; // Maximum distance (of closest screwdriver) to record sound

    public float maxPitchDist; // Maximum distance for pitch (should be less than max volume dist)

    public bool isOn;
    void Start()
    {
        beepAudioSource = beepObject.GetComponent<AudioSource>();
        onAudioSource = onObject.GetComponent<AudioSource>();
        offAudioSource = offObject.GetComponent<AudioSource>();
        TurnOn(); // Start with on, put first door near player
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle on/off
        if(Input.GetKeyDown(KeyCode.X)){
            if(beepAudioSource.mute){
                TurnOn();
            } else {
                TurnOff();
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

        GameObject[] doors = GameObject.FindGameObjectsWithTag(DOOR_TAG);
        foreach(GameObject s in doors){
            if(Vector3.Distance(s.transform.position, transform.position) < minDist){
                minDist = Vector3.Distance(s.transform.position, transform.position);
            }
        }

        GameObject[] monsters = GameObject.FindGameObjectsWithTag(MONSTER_TAG);
        foreach(GameObject s in monsters){
            if(Vector3.Distance(s.transform.position, transform.position) < minDist){
                minDist = Vector3.Distance(s.transform.position, transform.position);
            }

            // Check if force on
            if(Vector3.Distance(s.transform.position, transform.position) < forceOnDistance){
                if(!isOn){
                    TurnOn();
                }
            }
        }

        float ratio = (maxDist - minDist) / maxDist;
        float pitchRatio = (maxPitchDist - Mathf.Min(maxPitchDist, minDist)) / maxPitchDist;
        beepAudioSource.volume = ratio * ratio * maxVolume; // Square to not have full volume all the time
        beepAudioSource.pitch = 1 + pitchRatio * 2; // Range from 1 to 3
        
    }

    public void TurnOff(){
        isOn = false;
        beepAudioSource.mute = true;
        offAudioSource.Play();
    }

    public void TurnOn(){
        isOn = true;
        beepAudioSource.mute = false;
        onAudioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Changes player settings/world settings based on where the player is
*/
public class WorldCheckManager : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string PORTAL_CAMERA_TAG = "Portal Camera";

    private const string MUSIC_SOURCE = "Music Source";

    private GameObject portalCamera;
    public Material newSkybox;
    public Material newPortalSkybox;

    public AudioClip newMusic;

    private GameObject musicSource;

    void Start(){
        portalCamera = GameObject.FindGameObjectWithTag(PORTAL_CAMERA_TAG);
        musicSource = GameObject.FindGameObjectWithTag(MUSIC_SOURCE);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == PLAYER_TAG){
            Camera.main.GetComponent<Skybox>().material = newSkybox;
            portalCamera.GetComponent<Skybox>().material = newPortalSkybox;
            musicSource.GetComponent<AudioSource>().clip = newMusic;
            musicSource.GetComponent<AudioSource>().Play();
        }
    }
}

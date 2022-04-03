using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Changes player settings/world settings based on where the player is
*/
public class WorldCheckManager : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    public Material newSkybox;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == PLAYER_TAG){
            Camera.main.GetComponent<Skybox>().material = newSkybox;
        }
    }
}

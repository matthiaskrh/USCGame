using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public GameObject homeDoorway;
    private DoorScript homeDoorScript;

    public GameObject targetDoorway;
    private DoorScript targetDoorScript;


    // Start is called before the first frame update
    void Start()
    {
        homeDoorScript = homeDoorway.GetComponent<DoorScript>();
        targetDoorScript = targetDoorway.GetComponent<DoorScript>();
    }


    private void OnTriggerEnter(Collider other) {
        // Trying to teleport
        if(other.tag == PLAYER_TAG){ // Will only teleport player, for now
            if(targetDoorScript.getIsOpened()){ // Cannot teleport when target door is closed
                other.transform.position = targetDoorway.transform.position; // For now, have arrival location = doorway location (which is on the floor)
            }
        }
    }
}

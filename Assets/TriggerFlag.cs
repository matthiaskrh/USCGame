using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlag : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    private bool isTriggered = false;

    public bool getFlag(){
        return isTriggered;
    }
    
    void OnTriggerEnter(Collider other) {
         if (other.tag == PLAYER_TAG) {
             isTriggered = true;
         }
     }
     
     void OnTriggerExit(Collider other) {
         if (other.tag == PLAYER_TAG) {
             isTriggered = false;
         }
     }
}

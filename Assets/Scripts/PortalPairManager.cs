using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPairManager : MonoBehaviour
{
    public GameObject portal1;

    // Base
    public GameObject door1;

    // Target
    public GameObject door2;

    private DoorScript door1script;
    private DoorScript door2script;

    private Portal portal1script;


    void Start(){
        door1script = door1.GetComponent<DoorScript>();
        door2script = door2.GetComponent<DoorScript>();
        portal1script = portal1.GetComponent<Portal>();
    }


    void Update()
    {
        // Disabling portal
        if(isPortalActive()){
            portal1script.enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        } else {
            portal1script.enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    // Should portal be active
    public bool isPortalActive(){
        return isDoorValidEntrance(door1script) && isDoorValidTarget(door2script);
    }


    // If at least one door is unhinged
    public bool isPortalDisabled(){
        return door1script.getIsUnhinged() || door2script.getIsUnhinged();
    }

    bool isDoorValidTarget(DoorScript doorScript){
        return doorScript.getIsOpened() && !doorScript.getIsUnhinged();
    }

    bool isDoorValidEntrance(DoorScript doorScript){
        return doorScript.getIsOpened() && !doorScript.getIsUnhinged();
    }

    private void OnTriggerEnter(Collider other) {
        if(isPortalActive()){ // teleport to target
            other.transform.position = door2script.destinationObject.transform.position;
            other.transform.rotation = door2script.destinationObject.transform.rotation;
        }
    }
}

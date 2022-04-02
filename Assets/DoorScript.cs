using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public GameObject interactSphere;
    private TriggerFlag triggerFlag;
    public GameObject pivot;
    public float openedAngle = -90;
    public float closedAngle = 0;

    private bool isOpened = false;

    void Start(){
        triggerFlag = interactSphere.GetComponent<TriggerFlag>();
    }
    void Update()
    {
        bool canInteract = triggerFlag.getFlag();
        if(Input.GetKeyDown(KeyCode.E) && canInteract){
            if(isOpened){
                CloseDoor();
            } else {
                OpenDoor();
            }
        }
    }

    public bool getIsOpened(){
        return isOpened;
    }

    void OpenDoor(){
        isOpened = true;
        pivot.transform.eulerAngles = new Vector3(pivot.transform.eulerAngles.x, openedAngle, pivot.transform.eulerAngles.z);
    }

    void CloseDoor(){
        isOpened = false;
        pivot.transform.eulerAngles = new Vector3(pivot.transform.eulerAngles.x, closedAngle, pivot.transform.eulerAngles.z);
    }

    
}




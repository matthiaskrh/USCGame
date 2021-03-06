using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public GameObject destinationObject;
    public GameObject interactSphere;
    private TriggerFlag triggerFlag;
    private EnemyTriggerFlag enemyTriggerFlag;
    public GameObject pivot;
    public float openedAngle = -90;
    public float closedAngle = 0;

    public bool isOpened = false;
    private PlayerState playerState;
    public bool isUnhinged = false;

    public GameObject unhingedPivot;

    public GameObject doorColliderObject;
    private MeshRenderer doorCollider;

    void Start()
    {
        playerState = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerState>();
        triggerFlag = interactSphere.GetComponent<TriggerFlag>();
        enemyTriggerFlag = interactSphere.GetComponent<EnemyTriggerFlag>();
        doorCollider = doorColliderObject.GetComponent<MeshRenderer>();
        OpenDoor(); // All doors start open
    }

    void Update()
    {
        // Opening and closing door functionality
        bool canInteract = triggerFlag.getFlag();
        bool enemyCanInteract = enemyTriggerFlag.getFlag();

        if (enemyCanInteract && !isUnhinged)
        {
            if (!isOpened)
                OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.E) && canInteract && !isUnhinged)
        {
            if (isOpened)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }

        // Unhinging
        if(Input.GetKeyDown(KeyCode.R) && canInteract && !isUnhinged && playerState.hasScrewdriver){
            UnhingeDoor();
        }
    }

    public bool getIsOpened(){
        return isOpened;
    }

    public bool getIsUnhinged(){
        return isUnhinged;
    }

    void OpenDoor(){
        isOpened = true;
        pivot.transform.localEulerAngles = new Vector3(pivot.transform.eulerAngles.x, openedAngle, pivot.transform.eulerAngles.z);
    }

    void CloseDoor(){
        isOpened = false;
        pivot.transform.localEulerAngles = new Vector3(pivot.transform.eulerAngles.x, closedAngle, pivot.transform.eulerAngles.z);
    }

    public void UnhingeDoor(){
        isUnhinged = true;
        pivot.transform.eulerAngles = new Vector3(pivot.transform.eulerAngles.x, closedAngle, pivot.transform.eulerAngles.z);
        pivot.transform.position = unhingedPivot.transform.position;
        pivot.transform.rotation = unhingedPivot.transform.rotation;
        doorCollider.enabled = false;
    }

    
}




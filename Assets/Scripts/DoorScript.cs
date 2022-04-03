using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public GameObject destinationObject;
    public GameObject interactSphere;
    private TriggerFlag triggerFlag;
    private EnemyTriggerFlag enemyTriggerFlag;
    public GameObject pivot;
    public float openedAngle = -90;
    public float closedAngle = 0;

    private bool isOpened = false;

    public PlayerState playerState;
    public bool unhinged = false;
    public float disableTime = 5.0f;
    public float elapsedDisableTime = 0.0f;
    public bool isDisabling = false;

    void Start()
    {
        triggerFlag = interactSphere.GetComponent<TriggerFlag>();
        enemyTriggerFlag = interactSphere.GetComponent<EnemyTriggerFlag>();
        elapsedDisableTime = 0.0f;
        OpenDoor(); // All doors start open
    }

    void Update()
    {
        bool canInteract = triggerFlag.getFlag();
        bool enemyCanInteract = triggerFlag.getFlag();

        if (enemyCanInteract && !unhinged)
        {
            if (!isOpened)
                OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.E) && canInteract && !unhinged)
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

        else if (Input.GetKeyDown(KeyCode.T) && canInteract && !unhinged && playerState.hasScrewdriver &&
                 !isOpened)
        {
            isDisabling = true;
        }

        if (Input.GetKeyUp(KeyCode.T) && isDisabling)
        {
            isDisabling = false;
        }

        if (isDisabling)
        {
            if (elapsedDisableTime > disableTime)
            {
                unhinged = true;
                isDisabling = false;
                elapsedDisableTime = 0.0f;
            }
            else
                elapsedDisableTime += Time.deltaTime;
        }
        else
            elapsedDisableTime = 0.0f;
    }

    public bool getIsOpened(){
        return isOpened;
    }

    public bool getIsUnhinged(){
        return unhinged;
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




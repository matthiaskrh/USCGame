using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool hasScrewdriver;
    public GameObject heldScrewdriverSprite;
    private SpriteRenderer screwdriverSpriteRenderer;

    public bool isInOverworld;
    public GameObject overworldTriggerBox;
    private TriggerFlag overworldTriggerFlag;

    // Doors
    public Transform[] overworldDoorTransforms;
    public Transform[] underworldDoorTransforms;
    public Transform closestDoor;

    // Start is called before the first frame update
    void Start()
    {
        screwdriverSpriteRenderer = heldScrewdriverSprite.GetComponent<SpriteRenderer>();
        RemoveScrewdriver(); // Default state is no screwdriver
        overworldTriggerFlag = overworldTriggerBox.GetComponent<TriggerFlag>();
    }

    // Update is called once per frame
    void Update()
    {
        isInOverworld = overworldTriggerFlag.getFlag();

        if (isInOverworld)
        {
            if (overworldDoorTransforms.Length > 0)
            {
                closestDoor = overworldDoorTransforms[0];
                float closestDist = Vector3.Distance(closestDoor.position, transform.position);

                for (int i = 0; i < overworldDoorTransforms.Length; i++)
                {
                    float dist = Vector3.Distance(overworldDoorTransforms[i].position, transform.position);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestDoor = overworldDoorTransforms[i];
                    }
                }
            }
        }
        else
        {
            if (underworldDoorTransforms.Length > 0)
            {
                closestDoor = underworldDoorTransforms[0];
                float closestDist = Vector3.Distance(closestDoor.position, transform.position);

                for (int i = 0; i < underworldDoorTransforms.Length; i++)
                {
                    float dist = Vector3.Distance(underworldDoorTransforms[i].position, transform.position);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestDoor = underworldDoorTransforms[i];
                    }
                }
            }
        }
    }

    public void AddScrewdriver()
    {
        hasScrewdriver = true;
        screwdriverSpriteRenderer.enabled = true;
    }

    public void RemoveScrewdriver()
    {
        hasScrewdriver = false;
        screwdriverSpriteRenderer.enabled = false;
    }
}

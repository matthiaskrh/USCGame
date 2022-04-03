using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool hasScrewdriver;

    public bool isInOverworld;
    public GameObject overworldTriggerBox;
    private TriggerFlag overworldTriggerFlag;

    // Start is called before the first frame update
    void Start()
    {
        hasScrewdriver = false;
        overworldTriggerFlag = overworldTriggerBox.GetComponent<TriggerFlag>();
    }

    // Update is called once per frame
    void Update()
    {
        isInOverworld = overworldTriggerFlag.getFlag();
    }

    public void AddScrewdriver()
    {
        hasScrewdriver = true;
    }

    public void RemoveScrewdriver()
    {
        hasScrewdriver = false;
    }
}

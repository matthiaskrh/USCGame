using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    public GameObject doorway;
    private DoorScript doorScript;


    // Start is called before the first frame update
    void Start()
    {
        doorScript = doorway.GetComponent<DoorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

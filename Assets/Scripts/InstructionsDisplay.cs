using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsDisplay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            if(GetComponent<Text>().enabled){
                GetComponent<Text>().enabled = false;
            } else {
                GetComponent<Text>().enabled = true;
            }
        }
    }
}

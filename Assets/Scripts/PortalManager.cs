using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField]
    private Portal[] portals = new Portal[4];

    [SerializeField]
    private BasicPortalCamera basicPortalCamera;

    private int totalPair;
    private int pairNum;

    void Start(){
        totalPair = portals.Length / 2;
        pairNum = 0;
        for(int i = 2; i < portals.Length; i++){
            portals[i].setEnable(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            basicPortalCamera.setPortalsEnable(false);

            pairNum = (pairNum + 1) % totalPair;

            Portal prt1 =portals[pairNum*2];
            Portal prt2 =portals[pairNum*2+1];

            basicPortalCamera.setPortals(prt1, prt2);
            basicPortalCamera.setPortalsEnable(true);
        }
    }
}

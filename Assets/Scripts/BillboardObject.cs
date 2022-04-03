using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class BillboardObject : MonoBehaviour
 {

     private GameObject portalCamera;
     public const string PORTAL_CAMERA_TAG = "Portal Camera";

     public void Start(){
         portalCamera = GameObject.FindGameObjectWithTag(PORTAL_CAMERA_TAG);
     }
     private void LateUpdate()
     {
         /*
         Vector2 dif = new Vector2(Camera.main.transform.position.x - transform.position.x, Camera.main.transform.position.z - transform.position.z);
         transform.forward = new Vector3(dif.x, transform.forward.y, dif.y);
         */
         float portalCameraDist = Vector3.Distance(portalCamera.transform.position, transform.position);
         float playerCameraDist = Vector3.Distance(Camera.main.transform.position, transform.position);

        Transform targetCamTransform;
         if(portalCameraDist < playerCameraDist){
            targetCamTransform = portalCamera.transform;
         } else {
            targetCamTransform = Camera.main.transform;
         }


         transform.forward = new Vector3(targetCamTransform.forward.x, transform.forward.y, targetCamTransform.forward.z);
     }
 }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class BillboardObject : MonoBehaviour
 {
     private void LateUpdate()
     {
         /*
         Vector2 dif = new Vector2(Camera.main.transform.position.x - transform.position.x, Camera.main.transform.position.z - transform.position.z);
         transform.forward = new Vector3(dif.x, transform.forward.y, dif.y);
         */
         transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
     }
 }

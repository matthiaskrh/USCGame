using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobScript : MonoBehaviour
{
    public float bobSpeed;
    public float bobMagnitude;

    private Vector3 originalPosition;
    private float cycle = 0;

    void Start(){
        originalPosition = transform.localPosition;
    }
    void Update()
    {
        cycle += Time.deltaTime * bobSpeed;
        transform.localPosition = originalPosition + new Vector3(0, Mathf.Sin(cycle) * bobMagnitude, 0);

        if(cycle > 360){
            cycle = 0;
        }
    }
}

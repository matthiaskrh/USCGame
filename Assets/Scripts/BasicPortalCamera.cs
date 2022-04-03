using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPortalCamera : MonoBehaviour
{
    [SerializeField]
    private Portal[] portals = new Portal[2];

    [SerializeField]
    private Camera portalCamera;

    [SerializeField]
    private Material portalMaterial;

    private RenderTexture tempTexture;

    private Camera mainCamera;

    private const int maskID1 = 1;
    private const int maskID2 = 2;

    private GameObject[] closestPortals;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        tempTexture = new RenderTexture(Screen.width, Screen.height, 24);

        portalCamera.targetTexture = tempTexture;
    }

    private void Start()
    {
        portals[0].SetMaskID(maskID1);
        portals[1].SetMaskID(maskID2);
        closestPortals=GameObject.FindGameObjectsWithTag("Portal");
    }

    private void Update(){
        findClosestPortal();
    }

    public void setPortals(Portal prt0, Portal prt1){
        portals[0].SetMaskID(1);
        portals[1].SetMaskID(1);

        portals[0] = prt0;
        portals[1] = prt1;
        portals[0].SetMaskID(maskID1);
        portals[1].SetMaskID(maskID2);

    }

    public void setPortalsEnable(bool isEnabled){
        portals[0].setEnable(isEnabled);
        portals[1].setEnable(isEnabled);
    }

    private void findClosestPortal(){
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in closestPortals)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        Portal prt1  = closest.GetComponent<Portal>();
        Portal prt2 = prt1.GetOtherPortal();

        setPortals(prt1,prt2);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (!portals[0].IsPlaced() || !portals[1].IsPlaced())
        {
            Graphics.Blit(src, dst);
            return;
        }

        if (portals[0].IsRendererVisible())
        {
            // Render the first portal output onto the image.
            RenderCamera(portals[0], portals[1]);
            portalMaterial.SetInt("_MaskID", maskID1);
            Graphics.Blit(tempTexture, src, portalMaterial);
        }

        if(portals[1].IsRendererVisible())
        {
            // Render the second portal output onto the image.
            RenderCamera(portals[1], portals[0]);
            portalMaterial.SetInt("_MaskID", maskID2);
            Graphics.Blit(tempTexture, src, portalMaterial);
        }

        // Output the combined texture.
        Graphics.Blit(src, dst);
    }

    private void RenderCamera(Portal inPortal, Portal outPortal)
    {
        Transform inTransform = inPortal.transform;
        Transform outTransform = outPortal.transform;

        // Position the camera behind the other portal.
        Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
        relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
        portalCamera.transform.position = outTransform.TransformPoint(relativePos);

        // Rotate the camera to look through the other portal.
        Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
        relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
        portalCamera.transform.rotation = outTransform.rotation * relativeRot;
        
        // Set the camera's oblique view frustum.
        Plane p = new Plane(-outTransform.forward, outTransform.position);
        Vector4 clipPlane = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace =
            Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlane;

        var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        portalCamera.projectionMatrix = newMatrix;

        // Render the camera to its render target.
        portalCamera.Render();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public const string PORTAL_TAG = "Portal";
    public const string MAIN_SCENE_NAME = "MainScene";
    private GameObject[] portals;
    public GameObject winScreen;
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag(PORTAL_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        bool notOver = false;
        foreach(GameObject portal in portals){
            if(!portal.GetComponent<PortalPairManager>().isPortalDisabled()){
                notOver = true;
                break;
            }
        }

        if(notOver == false){
            StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(1);
        winScreen.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}

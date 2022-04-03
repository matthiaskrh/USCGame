using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public const string MONSTER_TAG = "Monster";
    public const string MAIN_SCENE_NAME = "MainScene";
    public GameObject deathImage;

    public GameObject deathSound;

    IEnumerator Death()
    {
        deathImage.GetComponent<Image>().enabled = true;
        deathSound.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
    

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == MONSTER_TAG){
            StartCoroutine(Death());
        }
    }
}

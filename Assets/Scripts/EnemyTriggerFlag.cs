using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerFlag : MonoBehaviour
{
    public const string ENEMY_TAG = "Monster";
    private bool isTriggered = false;

    public bool getFlag()
    {
        return isTriggered;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ENEMY_TAG)
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == ENEMY_TAG)
        {
            isTriggered = false;
        }
    }
}

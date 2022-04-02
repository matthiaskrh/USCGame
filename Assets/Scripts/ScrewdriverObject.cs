using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverObject : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public bool claimable;
    private Renderer renderer;
    public PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        claimable = true;
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trying to teleport
        if (claimable && other.tag == PLAYER_TAG)
        { // Will only teleport player, for now
            claimable = false;
            renderer.enabled = false;
            playerState.AddScrewdriver();
        }
    }
}

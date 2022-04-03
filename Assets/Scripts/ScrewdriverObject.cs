using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverObject : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public bool claimable;

    public GameObject screwdriverSprite;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        claimable = true;
        spriteRenderer = screwdriverSprite.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trying to teleport
        if (claimable && other.tag == PLAYER_TAG)
        { // Will only teleport player, for now
            claimable = false;
            spriteRenderer.enabled = false;

            PlayerState playerState = other.GetComponent<PlayerState>();
            playerState.AddScrewdriver();
        }
    }
}

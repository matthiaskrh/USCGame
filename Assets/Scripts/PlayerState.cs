using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool hasScrewdriver;
    public GameObject heldScrewdriverSprite;
    private SpriteRenderer screwdriverSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        screwdriverSpriteRenderer = heldScrewdriverSprite.GetComponent<SpriteRenderer>();
        RemoveScrewdriver(); // Default state is no screwdriver
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScrewdriver()
    {
        hasScrewdriver = true;
        screwdriverSpriteRenderer.enabled = true;
    }

    public void RemoveScrewdriver()
    {
        hasScrewdriver = false;
        screwdriverSpriteRenderer.enabled = false;
    }
}

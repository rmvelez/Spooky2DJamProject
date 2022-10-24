using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    [SerializeField] private float flashTimeInSeconds;
    [SerializeField] private Material flashMaterial;


    private float currentFlashTime = 0;
    private bool isFlashing = false;
    private Material swapBackMaterial;
    private SpriteRenderer spriteRenderer;


    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            currentFlashTime -= Time.deltaTime;

            if (currentFlashTime <= 0)
            {
                isFlashing = false;
                spriteRenderer.material = swapBackMaterial;
            }
        }
    }

    public void Flash(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
        if(!isFlashing) swapBackMaterial = spriteRenderer.material;
        spriteRenderer.material = flashMaterial;
        isFlashing = true;
        currentFlashTime = flashTimeInSeconds;
    }
}

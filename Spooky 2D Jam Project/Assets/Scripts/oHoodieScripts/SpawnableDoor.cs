using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableDoor : MonoBehaviour, ISpawnable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private UnityEngine.Experimental.Rendering.Universal.ShadowCaster2D shadowCaster;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private AudioSource audioSource;

    public void Spawn()
    {
        collider.enabled = true;
        spriteRenderer.enabled = true;
        shadowCaster.enabled = true;
        SoundBank.PlayAudioClip(SoundBank.GetInstance().DoorUnlockAudioClips, audioSource);
    }


}

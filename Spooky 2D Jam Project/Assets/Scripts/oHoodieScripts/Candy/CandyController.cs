using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyController : MonoBehaviour
{
    public float healAmount;
    [SerializeField] private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(-healAmount);

            SoundBank.PlayAudioClip(SoundBank.GetInstance().newItemAudioClips, audioSource);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this);
        }
    }
}

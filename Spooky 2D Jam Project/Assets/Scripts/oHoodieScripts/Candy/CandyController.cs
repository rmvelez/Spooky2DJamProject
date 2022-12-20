using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyController : MonoBehaviour
{
    public float healAmount;
    public bool isGolden = false;


    private void Start()
    {
        if (isGolden)
        {
            GetComponent<Animator>().SetBool("isGolden", true);
            gameObject.SetActive(false);
        }
    }


    [SerializeField] private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {

            // Normal Candy: Heal half a heart
            if (!isGolden)
            { 

                damagable.TakeDamage(-healAmount);

                SoundBank.PlayAudioClip(SoundBank.GetInstance().newItemAudioClips, audioSource);
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(this);
            }
            // Golden Candy: Win the game
            else
            {
                SceneManager.LoadScene("WinScene");
            }
        }

    }
}

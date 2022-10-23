using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDamageColliderController : MonoBehaviour
{
    [SerializeField] GhostController ghostController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable= collision.gameObject.GetComponent<IDamagable>();
        if(damagable != null){
            damagable.TakeDamage(ghostController.damage);
        }
    }
}

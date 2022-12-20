using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour, IDamagable
{
    [SerializeField] private BossController bossController;

    public void TakeDamage(float amount)
    {
        bossController?.TakeDamage(amount);
    }
}

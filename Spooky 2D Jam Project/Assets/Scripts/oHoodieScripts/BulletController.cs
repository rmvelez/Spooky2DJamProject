using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private bool CanHurtPlayer;
    [SerializeField] private bool CanHurtEnemy;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private new Collider2D collider;
    
    private Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(moveVector.x, moveVector.y, 0) * Time.deltaTime * speed;
    }

    /// <summary>
    /// Tell the bullet to move in the given direction until it hits another collider
    /// </summary>
    public void Shoot(Vector2 moveVector)
    {
        this.moveVector = moveVector.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveVector = Vector2.zero;
        animator.SetTrigger("Hit");
        collider.enabled = false;
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null) damagable.TakeDamage(damage);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}


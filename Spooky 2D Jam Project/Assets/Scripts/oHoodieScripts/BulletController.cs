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
        // Enemy Bullet hitting the player
        if (collision.gameObject.tag == "Player" && (CanHurtPlayer))
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null) damagable.TakeDamage(damage);

            Destroy(this.gameObject);
        }
        // Player bullet hitting the enemy
        else if (collision.gameObject.tag == "Enemy" && CanHurtEnemy)
        {
            moveVector = Vector2.zero;
            animator.SetTrigger("Hit");
            collider.enabled = false;

            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null) damagable.TakeDamage(damage);
        }
        // any bullet hitting a wall
        else if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy")
        {
            if (CanHurtEnemy)
            {
                moveVector = Vector2.zero;
                animator.SetTrigger("Hit");
                collider.enabled = false;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }


    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}


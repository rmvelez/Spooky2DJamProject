using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class SpikeController : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private new Collider2D collider;
    [SerializeField] private Animator animator;

    private bool firstFrameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Toggles the collider that deals damage on or off
    /// </summary>
    public void ToggleCollider()
    {
        collider.enabled = !collider.enabled;
    }

    /// <summary>
    /// Change Direction in which the animation is played, so that the spike returns into the ground.
    /// </summary>
    public void ChangeAnimDirection()
    {
        animator.SetFloat("speed", -1);
    }


    public void Despawn()
    {
        if (!firstFrameOver)
        {
            firstFrameOver = true;
            return;
        }

        Destroy(this.gameObject);
    }

    /// <summary>
    /// Deal damage if player collides with spike
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || collision.gameObject == null || collision.gameObject.tag != "Player") return;

        IDamagable damageable = collision.GetComponent<IDamagable>();
        if (damageable != null) damageable.TakeDamage(damage);
        
    }


}

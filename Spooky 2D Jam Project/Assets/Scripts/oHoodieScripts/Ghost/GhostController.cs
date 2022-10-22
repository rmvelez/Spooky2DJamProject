using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour, IDamagable
{
    [SerializeField] private float startingNrOfLives;
    [SerializeField] private ItemController lootPrefab;
    public float distanceToSpawn;
    public float alphaWhenVisible;
    public float fadeTime;
    public float appearDistance;
    public float damage;
    public float minAttackTime;
    public float maxAttackTime;
    public float minWalkTime;
    public float maxWalkTime;

    private float nrOfLives;
    private Vector3 stateStartPos;

    [HideInInspector] public bool hasWalkedBefore = false;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerController playerController;
    
    public new Collider2D collider;
    public  SpriteRenderer spriteRenderer;

    private GhostState ghostState;
    [HideInInspector] public Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nrOfLives = startingNrOfLives;
        ChangeGhostState(new GhostStateHidden(this));
        stateStartPos = transform.position;
    }

    void Update()
    {
        ghostState?.OnStateUpdate();

        Move();
        Fade();
    }


    private void Move()
    {
        // move to target position
        if (ghostState != null && ghostState.totalStateTime != 0)
        {
            float moveProgress = ghostState.currentStateTime / ghostState.totalStateTime;

            Vector3 offsetFromStateStart = (targetPosition - stateStartPos) * moveProgress;
            transform.position = stateStartPos + offsetFromStateStart;
        }
    }

    private void Fade()
    {
        if (ghostState.useFading && ghostState.totalStateTime != 0)
        {
            // Fade in
            if (ghostState.currentStateTime < fadeTime && hasWalkedBefore)
            {
                float fadeProgress = ghostState.currentStateTime / fadeTime;
                spriteRenderer.color = new Color(1, 1, 1, alphaWhenVisible * fadeProgress);
            }
            // Fade out
            else if (ghostState.currentStateTime > ghostState.totalStateTime - fadeTime)
            {

                float fadeProgress = (ghostState.currentStateTime - (ghostState.totalStateTime - fadeTime)) / fadeTime;
                spriteRenderer.color = new Color(1, 1, 1, alphaWhenVisible * (1 - fadeProgress));
            }
            // Fully Visible
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, alphaWhenVisible);
            }
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, alphaWhenVisible);
        }
    }


    void FixedUpdate()
    {
        ghostState?.OnStateFixedUpdate();
    }

    public void ChangeGhostState(GhostState newGhostState)
    {
        Debug.Log($"Change Ghost State [{ghostState} -> {newGhostState}]");
        stateStartPos = transform.position;

        if (ghostState != null) ghostState.OnStateExit();
        ghostState = newGhostState;
        ghostState.OnStateEnter();
                stateStartPos = transform.position;

    }


    public void TakeDamage(float amount)
    {
        Debug.Log("Ghost takes dmg!!!");
        nrOfLives -= amount;

        if (nrOfLives <= 0) ChangeGhostState(new GhostStateDying(this));
    }


    /// <summary>
    /// Called by animation event
    /// </summary>
    public void FinishedSpawning()
    {
        collider.enabled = true;
        ChangeGhostState(new GhostStateWalking(this));
    }

    /// <summary>
    /// Called by animation event when dying anim is finished
    /// </summary>
    public void FinishedDying()
    {
        Destroy(this.gameObject);
    }


}

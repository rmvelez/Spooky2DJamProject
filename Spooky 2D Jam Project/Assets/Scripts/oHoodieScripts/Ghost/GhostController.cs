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
    public float MoveSpeedInWalkState;
    public float moveSpeedInAttackState;
    public float appearDistance;
    public float damage;
    public float distanceForFullFade;


    private float nrOfLives;
    private Vector3 stateStartPos;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerController playerController;
    
    public new Collider2D collider;
    public  SpriteRenderer spriteRenderer;

    public enum FadeState
    {
        Visible,
        Invisible,
        FadingIn,
        FadingOut
    }
    [HideInInspector] public FadeState fadeState = FadeState.Visible;

    private GhostState ghostState;
    [HideInInspector] public Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nrOfLives = startingNrOfLives;
        stateStartPos = transform.position;
        ChangeGhostState(new GhostStateHidden(this));
    }

    void Update()
    {
        ghostState?.OnStateUpdate();

        // move to target position
        if(ghostState != null && ghostState.moveSpeed != 0)
        {
            Vector3 moveVector = (targetPosition - transform.position).normalized * ghostState.moveSpeed * Time.deltaTime;

            // Don't overshoot the target
            if (Vector3.Distance(transform.position, targetPosition) < moveVector.magnitude)
            {
                transform.position = targetPosition;
            }
            else
            {
                transform.position = transform.position + moveVector;
            }
        }


        // Detect a fade out start
        if(ghostState != null && ghostState.useFading && (fadeState == FadeState.Visible || fadeState == FadeState.FadingIn))
        {

            if (Vector3.Distance(transform.position, targetPosition) <= distanceForFullFade) fadeState = FadeState.FadingOut;
        }

        // set transparency according to fade state
        if (ghostState != null && ghostState.useFading || (fadeState == FadeState.Invisible || fadeState == FadeState.Visible))
        {
            switch (fadeState)
            {
                case FadeState.Visible:
                    spriteRenderer.color = new Color(1, 1, 1, alphaWhenVisible);
                    break;

                case FadeState.Invisible:
                    spriteRenderer.color = new Color(1, 1, 1, 0);
                    break;

                case FadeState.FadingIn:

                    // Set Fade Entry
                    float distanceFromStart = Vector3.Distance(stateStartPos, transform.position);
                    if (distanceFromStart <= distanceForFullFade)
                    {
                        //Debug.Log($"Fading in: {(distanceFromStart / distanceForFullFade) * alphaWhenVisible}");
                        spriteRenderer.color = new Color(1, 1, 1, (distanceFromStart / distanceForFullFade) * alphaWhenVisible);
                    }
                    else
                    {
                        fadeState = FadeState.Visible;
                    }

                    spriteRenderer.color = new Color(1, 1, 1, alphaWhenVisible);
                    break;

                case FadeState.FadingOut:
                    // Set Fade Outro
                    float distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
                    if (distanceFromTarget <= distanceForFullFade)
                    {
                        spriteRenderer.color = new Color(1, 1, 1, alphaWhenVisible - (distanceFromTarget / distanceForFullFade) * alphaWhenVisible);
                        //Debug.Log($"Fading out: {alphaWhenVisible - (distanceFromTarget / distanceForFullFade) * alphaWhenVisible}");

                    }
                    else
                    {
                        fadeState = FadeState.Invisible;
                    }
                    break;
                default:
                    break;
            }
        }

        Debug.Log($"Ghost Fade State: {fadeState}");

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

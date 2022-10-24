using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletonController : MonoBehaviour, IDamagable
{
    [SerializeField] private BulletController boneBulletPrefab;
    [SerializeField] private float startingNrOfLives;
    [SerializeField] private bool isBoss = false;

    [SerializeField] private GameObject lootGameObjectToActivate;
    public float distanceToSpawn;
    public float damage;
    public int minBoneThrows;
    public int maxBoneThrows;
    public float moveSpeed;
    public float minMoveTime;
    public float maxMoveTime;
    public float minTeleDistanceFromPlayer;
    public float maxTeleDistanceFromPlayer;


    private float nrOfLives;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public FlashController flashController;
    [HideInInspector] public PlayerController playerController;

    public new Collider2D collider;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;


    private SkeletonState skeletonState;
    [HideInInspector] public Vector3 targetPosition;



    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashController = GetComponent<FlashController>();
        nrOfLives = startingNrOfLives;
        ChangeSkeletonState(new SkeletonStateHidden(this));
    }


    public void ChangeSkeletonState(SkeletonState newSkeletonState)
    {
        //Debug.Log($"Change Skeleton State [{skeletonState} -> {newSkeletonState}]");

        if (skeletonState != null) skeletonState.OnStateExit();
        skeletonState = newSkeletonState;
        skeletonState.OnStateEnter();
    }

    void FixedUpdate()
    {
        skeletonState?.OnStateFixedUpdate();
    }

    void Update()
    { 
        skeletonState?.OnStateUpdate();
    }

    public void TakeDamage(float amount)
    {
        //Debug.Log("Skeleton takes dmg!!!");
        nrOfLives -= amount;
        flashController.Flash(spriteRenderer);
        if (nrOfLives <= 0) ChangeSkeletonState(new SkeletonStateDying(this));
    }

    /// <summary>
    /// Called by animation event
    /// </summary>
    public void FinishedSpawning()
    {
        collider.enabled = true;
        ChangeSkeletonState(new SkeletonStateBoneThrow(this));
    }

    /// <summary>
    /// Called by animation event when dying anim is finished
    /// </summary>
    public void FinishedDying()
    {
        MusicController.GetInstance().RemoveEnemy();
        if (lootGameObjectToActivate != null) lootGameObjectToActivate.gameObject.SetActive(true);

        if (isBoss)
        {
            SceneManager.LoadScene("WinScene");
        }
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Called by animation event when throw anim is finished
    /// </summary>
    public void FinishedThrowing()
    {
        if (skeletonState == null || skeletonState.GetType() != typeof(SkeletonStateBoneThrow)) return;

        ((SkeletonStateBoneThrow)skeletonState).IncreaseBoneCounter();
    }


    /// <summary>
    /// Called by animation event when throw anim is finished
    /// </summary>
    public void FinishedeleOut()
    {
        ChangeSkeletonState(new SkeletonStateTeleIn(this));

    }

    /// <summary>
    /// Called by animation event when throw anim is finished
    /// </summary>
    public void FinishedTeleIn()
    {
        ChangeSkeletonState(new SkeletonStateBoneThrow(this));

    }

    /// <summary>
    /// Called by animation event 
    /// </summary>
    public void ThrowBone()
    {
        BulletController bulletController = Instantiate(boneBulletPrefab, transform.position, transform.rotation);
        bulletController.Shoot(playerController.transform.position - transform.position);
        SoundBank.PlayAudioClip(SoundBank.GetInstance().skeletonAttackAudioClips, audioSource);

    }

}

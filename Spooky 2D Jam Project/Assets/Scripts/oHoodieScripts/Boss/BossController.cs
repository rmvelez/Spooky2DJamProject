using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamagable, ISpawnable
{
    // Tweakables
    [SerializeField] private List<GameObject> invokableEnemyPrefabs;
    [SerializeField] private float maxHp;

    public int minNrOfSpikeAttackRepetitions;
    public int maxNrOfSpikeAttackRepetitions;

    public float flyingSpeed;

    public float minRoomX; // Define location and size of the boss room (used to determine where boss can move and where enemies can be spawned in)
    public float minRoomY;
    public float maxRoomX;
    public float maxRoomY;

    public GameObject spikeSpawnerPrefab;
    public GameObject goldenCandy;

    // References
    [SerializeField] private Rigidbody2D rb;
    public Animator animator;
    //[SerializeField] private new Collider2D collider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private FlashController flashController;


    // Internal
    private BossState bossState;
    private float currentHp;
    [HideInInspector] public bool isSpawned;
    [HideInInspector] public bool justLanded; // used for making sure boss does not use fly twice in a row (dirty fix)


    private void Start()
    {
        currentHp = maxHp;

        ChangeBossState(new BossStateIdle(this));
    }


    private void Update()
    {
        bossState?.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        bossState?.OnStateFixedUpdate();
    }

    public void ChangeBossState(BossState newBossState)
    {
        Debug.Log($"Change Boss State [{bossState} -> {newBossState}]");
        if(bossState != null && bossState.GetType() == typeof(BossStateLanding))
        {
            justLanded = true;
        }
        else
        {
            justLanded = false;
        }

        if (bossState != null) bossState.OnStateExit();
        bossState = newBossState;
        bossState.OnStateEnter();
    }



    /// <summary>
    /// Called by the animator on the frame on which a new enemy should be spawned in
    /// </summary>
    public void OnInvokeEnemy()
    {
        int index = Random.Range(0, invokableEnemyPrefabs.Count);
        GameObject newEnemy = Instantiate(invokableEnemyPrefabs[index], (transform.position + PlayerController.GetInstance().transform.position) / 2, transform.rotation);
        ISpawnable spawnable = newEnemy.GetComponent<ISpawnable>();
        if (spawnable != null) spawnable.Spawn();

        Debug.Log($"Invoking a {invokableEnemyPrefabs[index]}");

        
    }

    /// <summary>
    /// Return to idle state after invoke anim is over
    /// </summary>
    public void OnInvokeAnimFinished()
    {
         ChangeBossState(new BossStateAboutToFly(this));
    }


    /// <summary>
    /// Transition into flying state after 'windup'
    /// </summary>
    public void OnAboutToFlyAnimFinished()
    {
        ChangeBossState(new BossStateFlying(this));
    }

    /// <summary>
    /// Return to idle state after landing anim is over
    /// </summary>
    public void OnLandingAnimFinished()
    {
        ChangeBossState(new BossStateIdle(this));
    }

    /// <summary>
    /// Transition into dying state after 'windup'
    /// </summary>
    public void OnAboutToDieAnimFinished()
    {
        ChangeBossState(new BossStateDying(this));
    }


    /// <summary>
    /// Called by the animator on the frame when the spikes should be released
    /// </summary>
    public void OnSpikeRelease()
    {
        //Debug.Log("OnSpikeRelease()");
        if (bossState.GetType() == typeof(BossStateSpikeAttack) && animator.GetFloat("speed") == 1)
        {
            ((BossStateSpikeAttack)bossState).OnSpikeRelease();
        }
    }

    public void OnSpikeAnimFirstFrame()
    {
        //Debug.Log("OnSpikeAnimFirstFrame()");
        animator.SetFloat("speed", 1);

        if (bossState.GetType() == typeof(BossStateSpikeAttack))
        {
            ((BossStateSpikeAttack)bossState).OnSpikeAnimFirstFrame();
        }
    }

    public void OnSpikeAnimLastFrame()
    {
        animator.SetFloat("speed", -1);
    }


    public void OnDyingLastFrame()
    {
        goldenCandy.transform.position = transform.position + new Vector3(0, -.5f, 0);
        goldenCandy.SetActive(true);
        goldenCandy.GetComponent<Animator>().SetBool("isGolden", true);
    }


    public void TakeDamage(float amount)
    {
        currentHp -= amount;
        //Debug.Log($"Boss took damage! {currentHp} HP remaining.");
        flashController.Flash(spriteRenderer);
        if (currentHp <= 0) ChangeBossState(new BossStateDying(this));
   }

    public void Spawn()
    {
        isSpawned = true;
        gameObject.SetActive(true);
    }
}

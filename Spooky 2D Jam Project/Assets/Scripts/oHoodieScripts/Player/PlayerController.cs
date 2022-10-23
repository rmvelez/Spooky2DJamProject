using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDamagable
{
    #region Singleton
    private static PlayerController instance;
    public static PlayerController GetInstance()
    {
        if (instance == null)
        {
            GameObject playerControllerGameObject = GameObject.Find("Player");
            if (playerControllerGameObject != null) instance = playerControllerGameObject.GetComponent<PlayerController>();
            if (instance == null) Debug.LogError("PlayerController Prefab is missing in the scene!");
        }
        return instance;
    }
    #endregion

    // Tweakables
    public float startingNrOfLives;
    public float moveSpeed;
    public float dashSpeed;
    public float dashCooldown;
    public float dashDuration;
    public float equippedItemDistance;
    public ItemController equippedItem;
    

    // References
    public List<BaseItem> inventory;
    public GameObject itemPivot;
    public AudioSource itemAudioSource;

    // Internal State
    private PlayerState playerState;
    [HideInInspector] public float currentDashCooldown = 0;
    [HideInInspector] public float nrOfLives;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public  Animator animator;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public Vector2 aimVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


        nrOfLives = startingNrOfLives;

        ChangePlayerState(new PlayerStateIdle(this));


        if (inventory.Count > 0) Equip(inventory[0]);

        PlayerUI.GetInstance().UpdateInventory();
        PlayerUI.GetInstance().UpdateLives();
    }


    public void ChangePlayerState(PlayerState newPlayerState)
    {
        //Debug.Log($"Change Player State [{playerState} -> {newPlayerState}]");
        if (playerState != null) playerState.OnStateExit();
        playerState = newPlayerState;
        playerState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDashCooldown > 0) currentDashCooldown -= Time.deltaTime;

        playerState?.OnStateUpdate();

        if(playerState != null && playerState.allowItemUse) UseItem();
        if (playerState != null && playerState.allowDashing) Dash();
        if(playerState != null && playerState.mirrorLeftRight) FlipLeftRight();

        ChangeItem();
        Aim();

    }

    void FixedUpdate()
    {
        playerState?.OnStateFixedUpdate();
    }


    public void PickUpItem(BaseItem baseItem)
    {
        inventory.Add(baseItem);
        if (inventory.Count == 1) Equip(baseItem);

        PlayerUI.GetInstance().UpdateInventory();
        SoundBank.PlayAudioClip(SoundBank.GetInstance().newItemAudioClips, itemAudioSource);

    }


    /// <summary>
    /// Scales the gameobject by -1 when walking left
    /// </summary>
    private void FlipLeftRight()
    {
        transform.localScale = new Vector3(1 * (aimVector.x < 0 ? -1 : 1), 1, 1);
    }


    private void Equip(BaseItem baseItem)
    {
        if(equippedItem != null) Destroy(equippedItem.gameObject);

        equippedItem = Instantiate(baseItem.prefab, itemPivot.transform).GetComponent<ItemController>();
        equippedItem.Equip();
        PlayerUI.GetInstance().UpdateInventory();
        SoundBank.PlayAudioClip(SoundBank.GetInstance().itemSwitchAudioClips, itemAudioSource);

    }


    private void UseItem()
    {
        Vector2 joystickRight = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
        if(equippedItem != null && (Input.GetMouseButton(0) || joystickRight.magnitude > 0.9f))
        {
            equippedItem.Use();
        }

    }

    private void Dash()
    {
        if (currentDashCooldown > 0) return;
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5)) ChangePlayerState(new PlayerStateDashing(this));
    }

    /// <summary>
    /// Equip another item
    /// </summary>
    private void ChangeItem()
    {

        // Scroll wheel to change item

        float scrollDelta = Input.mouseScrollDelta.y;

        float leftTrigger = Input.GetAxis("L2") + Input.GetAxis("L1");
        float rightTrigger = Input.GetAxis("R2") + Input.GetAxis("R1");
        float joystickDelta = leftTrigger - rightTrigger;

        if (scrollDelta != 0 || joystickDelta != 0)
        {
            // Determine currently equipped item
            int currentItemIndex = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == equippedItem.baseItem)
                {
                    currentItemIndex = i;
                    break;
                }
            }

            // Equip next / previous item
            if ((scrollDelta > 0 || joystickDelta > 0) && currentItemIndex > 0) Equip(inventory[currentItemIndex - 1]);
            if ((scrollDelta < 0 || joystickDelta < 0) && currentItemIndex < inventory.Count - 1) Equip(inventory[currentItemIndex + 1]);

        }

    }


    /// <summary>
    /// Set the position and rotation of the equipped item according to where the player is aiming
    /// </summary>
    private void Aim()
    {


        Vector2 joystickInput = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2") * (-1));

        // Get Input ( prefer Controller over Mouse)
        Vector2 mouseAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseAim = mouseAim - new Vector2(transform.position.x, transform.position.y);
        aimVector = joystickInput.magnitude > 0.3f ? joystickInput : mouseAim;


        if (equippedItem == null) return;

        // Little anim, recoil when use item
        float modifiedEquppedItemDistance = equippedItemDistance;
        if (equippedItem.currentCooldown > equippedItem.cooldown / 2)
        {
            modifiedEquppedItemDistance *= (equippedItem.currentCooldown / equippedItem.cooldown);
        }
        else
        {
            modifiedEquppedItemDistance *= 1 - (equippedItem.currentCooldown / equippedItem.cooldown);

        }

        equippedItem.transform.position = transform.position + new Vector3(aimVector.x, aimVector.y, 0).normalized * modifiedEquppedItemDistance;
        float angle = Vector2.Angle(Vector2.up, aimVector);
        equippedItem.transform.rotation = Quaternion.Euler(0, 0, (aimVector.x > 0 ? 360 - angle : angle));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        nrOfLives -= amount;
        if (nrOfLives < 0) nrOfLives = 0;
        PlayerUI.GetInstance().UpdateLives();

        if(amount > 0) SoundBank.PlayAudioClip(SoundBank.GetInstance().PlayerHurtAudioClips, audioSource);


        if (nrOfLives <= 0)
        {
            ChangePlayerState(new PlayerStateDying(this));
        }

    }


    /// <summary>
    /// Removes an item from the player's inventory and updates the UI
    /// Equips the next item
    /// </summary>
    /// <param name="baseItem"></param>
    public void RemoveItemFromInventory(BaseItem baseItem)
    {
        if (inventory.Contains(baseItem))
        {
            int index = inventory.IndexOf(baseItem);
            inventory.Remove(baseItem);
            Equip(inventory.Count - 1 >= index ? inventory[index] : inventory[index - 1]);
            PlayerUI.GetInstance().UpdateInventory();
        }
    }


    /// <summary>
    /// Called from the animator
    /// </summary>
    public void PlayFootstepSound()
    {
        //SoundBank.PlayAudioClip(SoundBank.GetInstance().indoorFootstepAudioClips, audioSource);
    }


    /// <summary>
    /// Called from the animator event
    /// </summary>
    public void ProceedToLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }
}

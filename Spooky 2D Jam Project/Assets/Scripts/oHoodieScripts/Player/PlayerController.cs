using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    public float maxNrOfLives;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float equippedItemDistance;
    public ItemController equippedItem;
    

    // References
    public List<BaseItem> inventory;
    public GameObject itemPivot;


    // Internal State
    private PlayerState playerState;
    private float nrOfLives;

    [HideInInspector] public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerState = new PlayerStateIdle(this);
        rb = GetComponent<Rigidbody2D>();
        nrOfLives = maxNrOfLives;
    }


    public void ChangePlayerState(PlayerState newPlayerState)
    {
        Debug.Log($"Change Player State [{playerState} -> {newPlayerState}]");
        if (playerState != null) playerState.OnStateExit();
        playerState = newPlayerState;
        playerState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        playerState?.OnStateUpdate();

        if(playerState != null && playerState.allowShooting) UseItem();

        if (playerState != null && playerState.allowDashing) Dash();

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
    }



    private void Equip(BaseItem baseItem)
    {
        if(equippedItem != null) Destroy(equippedItem.gameObject);

        equippedItem = Instantiate(baseItem.prefab, itemPivot.transform).GetComponent<ItemController>();
        equippedItem.Equip();
        PlayerUI.GetInstance().UpdateInventory();
    }


    private void UseItem()
    {
        if(equippedItem != null)
        {
            equippedItem.Use();
        }

        //Vector2 joystickInput = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2") * (-1));
        //bool firePressed = Input.GetMouseButton(0) || joystickInput.magnitude > 0.3f; // Input.GetKey("joystick 1 button 5");
        //if (hp > 0 && currentShotCooldown <= 0 && firePressed)
        //{
        //    // Get Input ( prefer Controller over Mouse)
        //    Vector2 mouseAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mouseAim = mouseAim - new Vector2(shotSpawn.transform.position.x, shotSpawn.transform.position.y);

        //    Vector2 aimVector = joystickInput.magnitude > 0.3f ? joystickInput : mouseAim;

        //    // Shoot
        //    ShotController shot = GameObject.Instantiate(shotPrefab).GetComponent<ShotController>();
        //    shot.gameObject.transform.position = shotSpawn.transform.position;
        //    shot.gameObject.transform.localScale = new Vector3(.5f, .5f, 1);
        //    shot.Shoot(true, shotSpeed, shotDamage, aimVector, null, 1f);
        //    currentShotCooldown = shotCooldown;
        //}

        //currentShotCooldown -= Time.deltaTime;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) ChangePlayerState(new PlayerStateDashing(this));
    }

    /// <summary>
    /// Equip another item
    /// </summary>
    private void ChangeItem()
    {

        // Scroll wheel to change item

        float scrollDelta = Input.mouseScrollDelta.y;

        if (scrollDelta != 0)
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
            if (scrollDelta < 0 && currentItemIndex > 0) Equip(inventory[currentItemIndex - 1]);
            if (scrollDelta > 0 && currentItemIndex < inventory.Count - 1) Equip(inventory[currentItemIndex + 1]);

        }

    }


    /// <summary>
    /// Set the position and rotation of the equipped item according to where the player is aiming
    /// </summary>
    private void Aim()
    {
        if (equippedItem == null) return;

        Vector2 joystickInput = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2") * (-1));

        // Get Input ( prefer Controller over Mouse)
        Vector2 mouseAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseAim = mouseAim - new Vector2(transform.position.x, transform.position.y);
        Vector2 aimVector = joystickInput.magnitude > 0.3f ? joystickInput : mouseAim;

        equippedItem.transform.position = transform.position + new Vector3(aimVector.x, aimVector.y, 0).normalized * equippedItemDistance;

        float angle = Vector2.Angle(Vector2.up, aimVector);
        Debug.Log($"Angle: {angle}");
        equippedItem.transform.rotation = Quaternion.Euler(0, 0, (aimVector.x > 0 ? 360 - angle : angle));
    }
}

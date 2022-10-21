using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxNrOfLives;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;

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

        if(playerState != null && playerState.allowShooting) Shoot();

        if (playerState != null && playerState.allowDashing) Dash();
        
    }

    private void FixedUpdate()
    {
        playerState?.OnStateFixedUpdate();
    }


    private void Shoot()
    {

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
}

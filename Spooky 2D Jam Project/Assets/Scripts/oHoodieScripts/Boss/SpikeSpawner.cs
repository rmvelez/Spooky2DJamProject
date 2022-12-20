using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class SpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;
    public float moveSpeed;
    public Vector2 moveVector;
    public float secondsPerSpike;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = secondsPerSpike;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the spawner
        transform.position = transform.position + new Vector3(moveVector.x, moveVector.y, 0) * moveSpeed * Time.deltaTime;

        // Spawn a spike if time reached
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = secondsPerSpike;
            Instantiate(spikePrefab, transform.position, transform.rotation);
        }

    }

    /// <summary>
    /// Destroy the spawner if it hits a wall
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}

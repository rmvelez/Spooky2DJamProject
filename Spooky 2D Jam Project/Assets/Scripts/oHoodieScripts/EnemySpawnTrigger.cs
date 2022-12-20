using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[RequireComponent (typeof(Collider2D))]

public class EnemySpawnTrigger : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        foreach (GameObject enemy in enemiesToSpawn)
        {
            enemy.GetComponent<ISpawnable>().Spawn();
        }

        Destroy(this);
    }

}

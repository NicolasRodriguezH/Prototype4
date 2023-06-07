using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    // Dado que no tenemos que controlar al jugador, sera una variable private
    private GameObject player;

    public bool isBoos = false;
    public float spawnInterval;
    private float nextSpawn;

    public int miniEnemySpawnCount;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if(isBoos)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if(isBoos)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;

                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}

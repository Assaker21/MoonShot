using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRain : MonoBehaviour
{
    public Transform SpawnPlace;
    public GameObject AlienFighter;
    public GameObject AlienDestroyer;
    public Transform Player;

    public float SpawnRate = 0f;

    float nextTimeToSpawn;
    int p = 0;

    private void Update()
    {
        if (nextTimeToSpawn < Time.time)
        {
            Spawn();
            nextTimeToSpawn = Time.time + 1 / SpawnRate;
        }
    }

    void Spawn()
    {
        p = Random.Range(0, 101);
        if (p < 20)
        {
            Instantiate(AlienDestroyer, SpawnPlace.position, Quaternion.Euler(Vector3.zero), transform);
        }
        else {
            Instantiate(AlienFighter, SpawnPlace.position, Quaternion.Euler(Vector3.zero), transform);
        }
    }
}

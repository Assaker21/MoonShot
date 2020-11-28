using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAsteroidSystem : MonoBehaviour
{
    public Transform[] Asteroids;
    public Transform Player;
    public float AsteroidRate = 1f;

    int i = 0;
    int[] k = new int[] {-1, 1};
    private void Start()
    {
        StartCoroutine(Sequence()); 
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2f);
        for (i = 0; i < Asteroids.Length; i++)
        {
            if (!Asteroids[i].gameObject.activeSelf)
            {
                Asteroids[i].gameObject.SetActive(true);
                Asteroids[i].position = new Vector3(k[Random.Range(0, 2)] * Random.Range(Player.position.x + 100, Player.position.x + 200),
                    k[Random.Range(0, 2)] * Random.Range(Player.position.y + 100, Player.position.y + 200),
                    k[Random.Range(0, 2)] * Random.Range(Player.position.z + 100, Player.position.z + 200));
                yield return new WaitForSeconds(1 / AsteroidRate);
            }
        }
        //doonce = true;
    }
}

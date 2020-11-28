using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAsteroidSystem : MonoBehaviour
{
    public Transform[] Asteroids;
    public CameraPan camPan;
    public Transform AsteroidGameObj;

    public float AsteroidMaxDrop = -20f;
    public float minXRange = -20f;
    public float maxXRange = 20f;
    public float AsteroidFireRate = 0.5f;

    private void Start()
    {
        StartCoroutine(AsteroidStart());

    }
    private void Update()
    {
        if (!camPan.Panned)
        {
            for (int i = 0; i < Asteroids.Length; i++)
            {
                if (Asteroids[i].position.z <= AsteroidMaxDrop)
                {
                    Asteroids[i].position = new Vector3(Random.Range(minXRange, maxXRange), 0, 75);
                }
            }
        }
        else {
            for (int i = 0; i < Asteroids.Length; i++)
            {
                if (Asteroids[i] != null && !Asteroids[i].gameObject.activeSelf) Destroy(Asteroids[i].gameObject);
            }
            if (AsteroidGameObj.childCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator AsteroidStart()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < Asteroids.Length; i++)
        {
            if (!camPan.Panned)
            {
                Asteroids[i].gameObject.SetActive(true);
                Asteroids[i].position = new Vector3(Random.Range(minXRange, maxXRange), 0, 75);
                yield return new WaitForSeconds(1 / AsteroidFireRate);
            }
        }
    }
}

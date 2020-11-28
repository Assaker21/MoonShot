using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public GameObject EnemyCluster1;
    public GameObject EnemyCluster2;
    public GameObject EnemyCluster3;
    public GameObject EnemyRain;

    public CameraPan camPan;
    public AlienDestroyerScript ADS1;
    public AlienDestroyerScript ADS2;
    public AlienDestroyerScript ADS3;

    public bool PlayerIsHit = false;

    bool doonce1 = true;
    bool doonce2 = true;
    bool doonce3 = true;
    private void Update()
    {
        if (ADS1.PlayerIsHit || ADS2.PlayerIsHit || ADS3.PlayerIsHit)
            PlayerIsHit = true;
        else PlayerIsHit = false;
        if (camPan.Pan)
        {
            EnemyCluster1.SetActive(true);
        }
        if (EnemyCluster1.activeSelf && EnemyCluster1.transform.childCount == 0 && doonce1)
        {
            EnemyCluster1.SetActive(false);
            EnemyCluster2.SetActive(true);
            doonce1 = false;
        }
        if (EnemyCluster2.activeSelf && EnemyCluster2.transform.childCount == 0 && doonce2)
        {
            EnemyCluster2.SetActive(false);
            EnemyCluster3.SetActive(true);
            doonce2 = false;
        }
        if (EnemyCluster3.activeSelf && EnemyCluster3.transform.childCount == 0 && doonce3)
        {
            EnemyCluster3.SetActive(false);
            EnemyRain.SetActive(true);
            doonce3 = false;
        }
    }
}

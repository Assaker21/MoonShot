using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    private Vector3 direction;
    private Quaternion desiredRotation;
    private Rigidbody rb;
    private Rigidbody ShotRb;
    private GameObject enemyShotShots;
    
    private float nextTimeToFire = 0;
    bool invisible = false;

    public Transform Player;
    public GameObject EnemyShot;
    public Transform EnemyShots;
    public GameObject Explosion;
    public GameObject ShotOrigin;
    
    public float ActivationDistance = 50f;
    public float ShootingDistance = 40f;
    public float RotationalSnappiness = 5f;
    public float Speed = 50f;
    public float FireRate = 20f;
    public float ShotSpeed = 1f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (Player == null) Player = GameObject.FindGameObjectWithTag("Player").transform;
        if (EnemyShots == null) EnemyShots = GameObject.FindGameObjectWithTag("EnemyShots").transform;
    }

    private void Update()
    {
        direction = (Player.position - transform.position).normalized;
        desiredRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * RotationalSnappiness);

        if (Vector3.Distance(Player.position, transform.position) <= ActivationDistance)
        {
            if (Vector3.Distance(Player.position, transform.position) >= ShootingDistance) rb.AddForce(direction * Speed * Time.deltaTime, ForceMode.VelocityChange);
            else if (Time.time >= nextTimeToFire)
            {
                rb.velocity = Vector3.zero;
                nextTimeToFire = Time.time + 1 / FireRate;
                enemyShotShots = Instantiate(EnemyShot, ShotOrigin.transform.position, ShotOrigin.transform.rotation, EnemyShots);
                enemyShotShots.GetComponent<Rigidbody>().AddForce(transform.forward * ShotSpeed, ForceMode.Impulse);
            }
        }
        else {
            rb.velocity = Vector3.zero;
        }
        if (Vector3.Distance(transform.position, Player.position) >= 250 && invisible)
        {
            Instantiate(Explosion, transform.position, transform.rotation, EnemyShots);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Shot" || collision.transform.tag == "Player")
        {
            Instantiate(Explosion, transform.position, transform.rotation, EnemyShots);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        invisible = true;
    }
    private void OnBecameVisible()
    {
        invisible = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDestroyerScript : MonoBehaviour
{
    private Vector3 direction;
    private Quaternion desiredRotation;
    private Rigidbody rb;
    private ParticleSystem.Particle[] Particle;

    private float nextTimeToFire = 0;
    private int numPartAlive;
    private ParticleSystem.MainModule main;
    private RaycastHit hit;

    public Transform Player;
    public GameObject Explosion;
    public GameObject EnemyShots;
    public ParticleSystem Railgun;

    public float ActivationDistance = 50f;
    public float ShootingDistance = 40f;
    public float RotationalSnappiness = 5f;
    public float Speed = 50f;
    public float FireRate = 20f;
    public bool PlayerIsHit = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (Player == null) Player = GameObject.FindGameObjectWithTag("Player").transform;
        if (EnemyShots == null) EnemyShots = GameObject.FindGameObjectWithTag("EnemyShots");
        if (Particle == null || Particle.Length < Railgun.main.maxParticles)
            Particle = new ParticleSystem.Particle[Railgun.main.maxParticles];
        main = Railgun.main;
    }

    private void Update()
    {
        direction = (Player.position - transform.position).normalized;
        desiredRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * RotationalSnappiness);
        numPartAlive = Railgun.GetParticles(Particle);

        if (Vector3.Distance(Player.position, transform.position) <= ActivationDistance)
        {
            if (Vector3.Distance(Player.position, transform.position) >= ShootingDistance) rb.AddForce(direction * Speed * Time.deltaTime, ForceMode.VelocityChange);
            else if (Time.time >= nextTimeToFire)
            {
                rb.velocity = Vector3.zero;
                nextTimeToFire = Time.time + 1 / FireRate;
                Railgun.Play();
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        if (numPartAlive > 0)
        {
            if (Physics.Raycast(Railgun.gameObject.transform.position, Railgun.gameObject.transform.forward, out hit,
                main.startSpeed.constant * (main.startLifetime.constant - Particle[0].remainingLifetime)))
            {
                if (hit.transform.tag == "Player")
                {
                    Debug.Log("HIT");
                    PlayerIsHit = true;
                }
                else PlayerIsHit = false;
            }
            else PlayerIsHit = false;

        }
        else PlayerIsHit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Shot" || collision.transform.tag == "Player")
        {
            Instantiate(Explosion, transform.position, transform.rotation, EnemyShots.transform);
            Destroy(gameObject);
        }
    }
}

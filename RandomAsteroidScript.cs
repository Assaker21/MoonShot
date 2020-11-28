using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAsteroidScript : MonoBehaviour
{
    public Transform Player;
    public GameObject Child;
    public GameObject FracturedAsteroid;
    public GameObject Explosion;
    public float Speed = 50f;

    private Rigidbody rb;
    private RAsteroidScript AS;
    private GameObject Debris;
    private GameObject exp;
    public bool Invisible = false;
    bool keepGoing = false;
    bool Hit = false;
    readonly int[] k = new int[] {-1, 1};
    Vector3 PlayerPos;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        AS = Child.GetComponent<RAsteroidScript>();
        Vector3 yeet = new Vector3(k[Random.Range(0, 2)] * Random.Range(Player.position.x + 150, Player.position.x + 200),
                        k[Random.Range(0, 2)] * Random.Range(Player.position.y + 150, Player.position.y + 200),
                        k[Random.Range(0, 2)] * Random.Range(Player.position.z + 150, Player.position.z + 200));
        transform.position = yeet;
        if (!Invisible)
        {
            transform.position = -yeet;
        }
    }
    private void Update()
    {
        if (Child.activeSelf)
            if (Vector3.Distance(Player.position, transform.position) > 150 && !keepGoing)
            {
                PlayerPos = (Player.position - transform.position).normalized;
                rb.AddForce(PlayerPos * Speed * Time.deltaTime);
                keepGoing = false;
            }
            else
            {
                keepGoing = true;
                rb.AddForce(PlayerPos * Speed * Time.deltaTime);
            }
        if(Hit)
        {
            Hit = false;
            Child.SetActive(false);
            Debris = Instantiate(FracturedAsteroid, Child.transform.position, Child.transform.rotation, transform);
            exp = Instantiate(Explosion, Child.transform.position, Child.transform.rotation, transform);
            Destroy(Debris, 2f);
            Destroy(exp, 2f);
        }
        if (!Child.activeSelf && Invisible)
        {
            transform.position = new Vector3(k[Random.Range(0, 2)] * Random.Range(Player.position.x + 150, Player.position.x + 200),
                        k[Random.Range(0, 2)] * Random.Range(Player.position.y + 150, Player.position.y + 200),
                        k[Random.Range(0, 2)] * Random.Range(Player.position.z + 150, Player.position.z + 200));
            Child.SetActive(true);
            keepGoing = false;
            rb.velocity = Vector3.zero;
        }
        if (Vector3.Distance(transform.position, Player.position) >= 150 && keepGoing && Child.activeSelf && Invisible)
        {
            Child.SetActive(false);
        }

    }
    private void OnBecameInvisible()
    {
        Invisible = true;      
    }
    private void OnBecameVisible()
    {
        Invisible = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Shot")
        {
            Hit = true;
        }
    }
}

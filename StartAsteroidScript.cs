using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAsteroidScript : MonoBehaviour
{
    public Transform Player;
    public GameObject Explosion6;
    public GameObject Explosion7;
    public GameObject Explosion8;
    public CameraPan CamPan;
        
    public float Speed = 1f;
    
    Rigidbody rb;
    Vector3 PlayerPos;
    Vector3 MyPos;
    bool doonce = true;
    bool Invisible = false;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!CamPan.Panned)
        {
            if (transform.position.z <= -20)
            {
                rb.velocity = Vector3.zero;
            }
            if (transform.position.z >= 70)
            {
                rb.AddForce((Player.position - transform.position).normalized * Speed * Time.deltaTime);
                PlayerPos = Player.position;
                MyPos = transform.position;
                if (doonce)
                {
                    rb.velocity = Vector3.zero;
                    doonce = false;
                }
            }
            else
            {
                doonce = true;
                rb.AddForce((PlayerPos - MyPos).normalized * Speed * Time.deltaTime);
            }
        }
        else if(CamPan.Panned && Invisible)
        {
            rb.AddForce((PlayerPos - MyPos).normalized * Speed * Time.deltaTime);
            if (Vector3.Distance(Player.position, transform.position) >= 50 && Invisible)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Shot")
        {
            int i = Random.Range(1, 4);
            switch (i) {
                case 1:
                    GameObject k6 = Instantiate(Explosion6, collision.transform.position, Quaternion.Euler(Vector3.zero));
                    Destroy(k6, 2f);
                    break;
                case 2:
                    GameObject k7 = Instantiate(Explosion7, collision.transform.position, Quaternion.Euler(Vector3.zero));
                    Destroy(k7, 2f);
                    break;
                case 3:
                    GameObject k8 = Instantiate(Explosion8, collision.transform.position, Quaternion.Euler(Vector3.zero));
                    Destroy(k8, 2f);
                    break;
            }            
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
}

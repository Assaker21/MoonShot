using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionScript : MonoBehaviour
{
    public float DerailTorque = 100f;
    public MoneySystem Money;

    private Rigidbody rb;
    private bool doonce = true;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Money = GameObject.FindGameObjectWithTag("MoneySystem").GetComponent<MoneySystem>();
        Destroy(gameObject, 2f);
    }
    private void FixedUpdate()
    {
        /*RaycastHit hit1;
        if (Physics.SphereCast(transform.position, 2, transform.forward, out hit1))
        {
            if (hit1.transform.tag == "RAsteroid")
            {
                rb.AddForce((hit1.transform.position - transform.position).normalized * DerailTorque * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }*/
        /*RaycastHit hit2;
        if (Physics.SphereCast(transform.position, 7, new Vector3(1, 1, 1), out hit2))
        {
            if (hit2.transform.tag == "AlienFighter")
            {
                rb.velocity = Vector3.zero;
                rb.AddForce((hit2.transform.position - transform.position).normalized * DerailTorque * Time.fixedDeltaTime);
            }
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "AlienFighter")
        {
            if (doonce)
            {
                Money.MoneyEarned += 300;
                doonce = false;
                Destroy(gameObject);                
            }
        }
        if (collision.transform.tag == "Asteroid" || collision.transform.tag == "RAsteroid")
        {
            if (doonce)
            {
                Money.MoneyEarned += 25;
                doonce = false;
            }
        }
        else
        {
            doonce = true;
        }
    }
}


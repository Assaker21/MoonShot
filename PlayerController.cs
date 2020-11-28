using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody shipRb;
    private GameObject ShotObj;

    public Transform CameraIsHere;
    public Transform LeftShotOrigin;
    public Transform RightShotOrigin;
    public Transform Shots;
    public MeshRenderer mr;
    public MeshRenderer mr2;
    public MeshRenderer mr3;
    public GameObject flames;
    public ParticleSystem Flame, FlameLeft, FlameRight;
    public ParticleSystem SpeedLines;
    public GameObject Shot;
    public Animation NegativeTurret;
    public Animation PositiveTurret;
    public TrailRenderer LeftTrail;
    public TrailRenderer RightTrail;
    public Slider HealthBar;
    public Slider FireRateBar;
    public CameraPan camPan;
    public CameraShake CamShake2D;
    public EnemySystem enemySystem;
    public VisualEffect VFX;
    public GameObject DeathExplosion;
    public Image im;
    public Color c;

    public float ForwardSpeed = 100f;
    public float SteeringSpeed = 100f;
    public float SpeedBoostMultiplier = 50f;
    public float SpeedBoostRotationSpeed = 50f;
    public float ShotSpeed = 100f;
    public float TrailCutOff = 5f;
    public float TrailLength = 0.5f;
    public float ChangeSpeed = 5f;
    public float SpeedLinesMultiplier = 1f;
    public float Health = 1000f;
    public float FireRate = 2f;
    public int HealthPack = 90;
    public float VFXPlayRate = 1f;
    public Vector3 RotationA;
    public Vector3 RotationB;
    
    private int i = 1;
    private bool weeta = false;
    private bool Boosted = false;
    private bool doonce = true;
    private bool doonce2 = true;
    private float time3 = 0f;
    private float time2 = 0f;
    private float time = 0f;
    private float newTime = 0f;
    private float nextTimeToFire = 0f;
    private float totalTime = 0f;
    private Quaternion OriginalRotation;

    
    private void Start()
    {
        shipRb = gameObject.GetComponent<Rigidbody>();
        HealthBar.value = Health;
        VFX.playRate = VFXPlayRate;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if (yeet)
        {
            im.color = Color.Lerp(im.color, c, Time.deltaTime);
            if (im.color.a > 0.99f)
            {
                SceneManager.LoadScene("Win");
            }
        }
        if (Health <= 0)
        {
            if (doonce2)
            {
                mr.enabled = false;
                mr2.enabled = false;
                mr3.enabled = false;
                mr.enabled = false;
                flames.SetActive(false);
                Instantiate(DeathExplosion, transform.position, transform.rotation, transform);
                time3 = Time.time;
                doonce2 = false;
            }
            if (Time.time - time3 > 1)
            {
                SceneManager.LoadScene("Retry");
            }
        }
        else
        {
            if (enemySystem.PlayerIsHit && doonce)
            {
                Health -= 300;
                doonce = false;
                time2 = Time.time;
            }
            if (!doonce && Time.time - time2 > 1f)
            {
                doonce = true;
            }
            if (!camPan.Panned)
            {
                if (transform.position.x <= -64)
                {
                    transform.position = new Vector3(63.9f, transform.position.y, transform.position.z);
                }
                else if (transform.position.x >= 64)
                {
                    transform.position = new Vector3(-63.9f, transform.position.y, transform.position.z);
                }
                if (transform.position.z < -3.8f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -3.8f);
                }
                else if (transform.position.z > 58.4f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 58.4f);
                }
            }

            totalTime += Time.deltaTime;
            if (totalTime > SpeedBoostRotationSpeed)
            {
                totalTime = SpeedBoostRotationSpeed;
            }
            var emission = SpeedLines.emission;
            var mainLines = SpeedLines.main;
            emission.rateOverTime = shipRb.velocity.magnitude * SpeedLinesMultiplier;
            mainLines.startSpeed = Mathf.Sqrt(Mathf.Sqrt(shipRb.velocity.magnitude)) * 10;
            if (shipRb.velocity.magnitude >= TrailCutOff)
            {
                LeftTrail.time = Mathf.Lerp(LeftTrail.time, TrailLength, Time.smoothDeltaTime * ChangeSpeed);
                RightTrail.time = LeftTrail.time;
            }
            else if (shipRb.velocity.magnitude < TrailCutOff)
            {
                LeftTrail.time = Mathf.Lerp(LeftTrail.time, 0, Time.smoothDeltaTime * ChangeSpeed);
                RightTrail.time = LeftTrail.time;
            }
            if (Time.time - time >= 5f)
            {
                weeta = true;
            }
            else
            {
                weeta = false;
            }
            var main = Flame.main;
            var mainLeft = FlameLeft.main;
            var mainRight = FlameRight.main;
            float forwardSpeedNormalized = ForwardSpeed * Time.smoothDeltaTime;
            if (Input.GetKey(KeyCode.W))
            {
                shipRb.AddForce(CameraIsHere.forward * forwardSpeedNormalized);
                main.startLifetime = Mathf.Lerp(main.startLifetime.constant, 0.7f, Time.smoothDeltaTime);
                mainLeft.startLifetime = Mathf.Lerp(main.startLifetime.constant, 0.7f, Time.smoothDeltaTime);
                mainRight.startLifetime = Mathf.Lerp(main.startLifetime.constant, 0.7f, Time.smoothDeltaTime);
            }
            else
            {
                main.startLifetime = Mathf.Lerp(main.startLifetime.constant, 0.2f, Time.smoothDeltaTime);
                mainLeft.startLifetime = Mathf.Lerp(main.startLifetime.constant, 0.2f, Time.smoothDeltaTime);
                mainRight.startLifetime = Mathf.Lerp(main.startLifetime.constant, 0.2f, Time.smoothDeltaTime);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && weeta)
            {
                shipRb.AddForce(transform.forward * forwardSpeedNormalized * SpeedBoostMultiplier, ForceMode.Impulse);

                time = Time.time;
                newTime = Time.time;
                Boosted = true;
                totalTime = 0f;
                OriginalRotation = transform.rotation;
                weeta = false;
            }
            if (Boosted && Time.time - newTime <= 1f)
            {
                Vector3 temp = Vector3.Lerp(RotationA, RotationB, totalTime / SpeedBoostRotationSpeed);
                transform.rotation = OriginalRotation * Quaternion.Euler(temp);

            }
            else
            {
                Boosted = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                shipRb.AddForce(CameraIsHere.forward * -forwardSpeedNormalized);
            }
            transform.localRotation = Quaternion.Slerp(transform.localRotation, CameraIsHere.localRotation, SteeringSpeed * Time.smoothDeltaTime);
            if (Input.GetKey(KeyCode.A))
            {
                shipRb.AddForce(-transform.right * Time.smoothDeltaTime * ForwardSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                shipRb.AddForce(transform.right * Time.smoothDeltaTime * ForwardSpeed);
            }
            if (Input.GetKey(KeyCode.Space) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                if (!camPan.Panned)
                    CamShake2D.Shake();
                i = -i;
                if (i < 0)
                {
                    NegativeTurret.Play();
                    ShotObj = Instantiate(Shot, LeftShotOrigin.position, Quaternion.Euler(Vector3.zero), Shots);
                }
                else if (i > 0)
                {
                    PositiveTurret.Play();
                    ShotObj = Instantiate(Shot, RightShotOrigin.position, Quaternion.Euler(Vector3.zero), Shots);
                }
                ShotObj.GetComponent<Rigidbody>().AddForce(transform.forward * ShotSpeed, ForceMode.Impulse);
            }
        }
    }
    bool yeet = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "AlienShot")
        {
            Health -= 25f;
            HealthBar.value = Health;
        }
        else if (collision.transform.tag == "Asteroid")
        {
            Health -= 20f;
            HealthBar.value = Health;
        }
        else if (collision.transform.tag == "Shot" || collision.transform.tag == "NoDamage") { }
        else if (collision.transform.tag == "Health")
        {
            Destroy(collision.gameObject);
            Health += HealthPack;
            HealthBar.value = Health;
        }
        else if (collision.transform.tag == "FireRate")
        {
            Destroy(collision.gameObject);
            FireRate += 5;
        }
        else if (collision.transform.tag == "Sphere") yeet = true;
        else
        {
            Health -= 10f;
            HealthBar.value = Health;
        }
    }
                
}

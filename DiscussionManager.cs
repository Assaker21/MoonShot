using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiscussionManager : MonoBehaviour
{
    public AudioSource WomenCall;
    public AudioSource Discussion;
    public AudioSource Noise;
    public float speedd = 1f;
    public LookAtCamera AnimScript;
    public GameObject Loading;
    public Image im;
    public Color c;
    public Transform[] PS;
    public GameObject SubManager;

    bool doonce = true;
    float time = 0f;
    int i;
    private void Start()
    {
        Invoke("CallOn", 2f);
        Noise.Play();
    }
    private void Update()
    {
        if (AnimScript.stopAnim)
        {
            if (doonce)
            {
                time = Time.time;
                WomenCall.Stop();
                Discussion.Play();
                SubManager.SetActive(true);
                doonce = false;
            }
            Noise.volume = Mathf.Lerp(Noise.volume, 0.1f, speedd * Time.deltaTime);
            
            if (!Discussion.isPlaying)
            {
                im.color = Color.Lerp(im.color, c, Time.deltaTime);
                if (c.a - im.color.a <= 0.01f)
                {
                    NewScene();
                }
            }
            if (Time.time - time > 158f)
            {
                BlindingLight();
            }
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            NewScene();
        }
        
    }
    void CallOn()
    {
        WomenCall.Play();
    }
    void NewScene()
    {
        Loading.SetActive(true);
        SceneManager.LoadSceneAsync("PlayScene");
    }
    void BlindingLight()
    {        
        for (i = 0; i < PS.Length; i++)
        {
            PS[i].localScale = new Vector3(PS[i].localScale.x, Mathf.Lerp(PS[i].localScale.y,0.7f, Time.deltaTime), PS[i].localScale.z);
        }
        if (i >= PS.Length)
        {
            i = 0;
        }
    }
}

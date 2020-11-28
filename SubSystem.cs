using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubSystem : MonoBehaviour
{
    public Text Subs;
    public Image im;
    public Color HalfBlack;
    public float multiplier = 3f;
    public float[] s;
    public string[] t;
    void Start()
    {
        StartCoroutine(Sequence());
    }
    private void Update()
    {
        im.color = Color.Lerp(im.color, HalfBlack, Time.deltaTime);
    }
    IEnumerator Sequence()
    {
        for (int i = 0; i < s.Length; i++)
        {
            Subs.text = t[i];
            yield return new WaitForSeconds(s[i]);
        }
        Subs.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public Image im;
    public Color c;
    public GameObject butt;
    private void Start()
    {
        StartCoroutine(but());
    }
    private void Update()
    {
        im.color = Color.Lerp(im.color, c, Time.deltaTime);
    }

    public void exit()
    {
        Application.Quit();
    }

    IEnumerator but()
    {
        yield return new WaitForSeconds(2);
        butt.SetActive(true);
    }
}

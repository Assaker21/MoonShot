using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform Camera;
    public GameObject Text;
    public Transform Radio;
    public Transform Player;
    public float MaxDis = 5f;
    public float AnimationSpeed = 1f;
    public Vector3 PlayerOffset;
    public Vector3 RotationOfRadio;

    bool doonce = true;
    bool anim = false;
    public bool stopAnim = false;
    void Update()
    {
        transform.LookAt(Camera);
        if (Vector3.Distance(Radio.position, Player.position) <= MaxDis && doonce && !stopAnim)
        {
            Text.SetActive(true);
            doonce = false;
        }
        else if((Vector3.Distance(Radio.position, Player.position) >= MaxDis && !doonce) || stopAnim)
        {
            Text.SetActive(false);
            doonce = true;
        }
        if (!doonce && Input.GetKeyDown(KeyCode.E) && !stopAnim)
        {

            anim = true;
        }
        if (anim && !stopAnim)
        { 
            Animate();
        }
        if (stopAnim)
        {
            Radio.parent = Player;
        }
    }
    public void Animate()
    {
        Radio.position = Vector3.Lerp(Radio.position, Player.position + PlayerOffset, AnimationSpeed * Time.deltaTime);
        Radio.rotation = Quaternion.Slerp(Radio.rotation, Quaternion.Euler(RotationOfRadio), AnimationSpeed * Time.deltaTime);
        if (Vector3.Distance(Radio.position, Player.position + PlayerOffset) <= 0.01f)
        {
            Radio.position = Player.position + PlayerOffset;
            stopAnim = true;
        }
    }
}

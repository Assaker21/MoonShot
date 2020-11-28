using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheMarker : MonoBehaviour
{
    public Transform Player;
    public Image Arrow;

    private Color TargetAlpha;
    private float distance;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, Player.position);
        if (distance <= 1000)
        {
            if (distance < 100)
            {
                distance = 100;
            }
            TargetAlpha = new Vector4(Arrow.color.r, Arrow.color.g, Arrow.color.b, (distance - 100) / 1000);
            Arrow.color = TargetAlpha;
        }
        transform.LookAt(Player.position);
    }
}

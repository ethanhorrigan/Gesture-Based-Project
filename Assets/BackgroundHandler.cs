using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Set the start of the background (Xstart).
/// Set the end of the background (Xend).
/// Translate the background on the X axis.
/// When the position of the background is less the Xend, reset the image.
/// 
/// </summary>
public class BackgroundHandler : MonoBehaviour
{
    public float speed;
    public float Xend;
    public float Xstart;
    public static bool moving = true;

    private void Update()
    {
        if (moving)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < Xend)
            {
                Vector2 pos = new Vector2(Xstart, transform.position.y);
                transform.position = pos;
            }
        }
    }
}

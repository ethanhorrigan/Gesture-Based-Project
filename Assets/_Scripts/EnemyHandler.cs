using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public float speed;
    public static bool moving = true;

    void Update()
    {
        if (moving)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().health--;
            Destroy(gameObject);
        }
    }
}

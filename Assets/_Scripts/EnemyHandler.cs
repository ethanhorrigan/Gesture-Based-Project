using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test");
        if (other.CompareTag("Player"))
        {
            Debug.Log("test");
            other.GetComponent<Player>().health--;
            Destroy(gameObject);
        }
    }
}

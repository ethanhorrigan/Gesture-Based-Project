using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed = 3f;
    float jumpSpeed = 500f;
    public static bool grounded = true;
    bool jumping = false;
    Rigidbody2D rb;

    public int velocity;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (velocity == 0)
        {
            jumping = false;
            grounded = true;
        }

        if (grounded)
        {
            rb.gravityScale = 2.1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W) && velocity == 0)
        {
            if (grounded)
            {
                rb.AddForce(Vector3.up * jumpSpeed);
                grounded = false;
                jumping = true;
                Debug.Log("Vector3.up: " + Vector3.up + "");
            }
        }

        if (jumping == true)
        {
            velocity++;
        } 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        velocity = 0;
    }


}

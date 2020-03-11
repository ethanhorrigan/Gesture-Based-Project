using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    SpriteRenderer sr;
    GameObject player;
    bool playerNear = false;
    bool lock1 = false;
    bool lock2 = false;
    bool lock3 = false;

    public

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Checking player near");
        CheckPlayerNear();
    }

    private void CheckPlayerNear()
    {
        Debug.Log("called Player Near");

        if (player.transform.position.x > sr.gameObject.transform.position.x - 3 && player.transform.position.x < sr.gameObject.transform.position.x + 3)
        {
            Debug.Log("Player Near");
            playerNear = true;
        }
        else
        {
            Debug.Log("Player not Near");
            playerNear = false;
        }

        if (playerNear)
        {
            if (CheckPassword())
            {
                Debug.Log("Turn off Wall");
                sr.gameObject.SetActive(false);
                lock1 = false; lock2 = false; lock3 = false;
            }
        }
        else
        {
            sr.gameObject.SetActive(true);
        }
    }

    private Boolean CheckPassword()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            lock1 = true;
            Debug.Log("Lock 1 set");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            lock2 = true;
            Debug.Log("Lock 2 set");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            lock3 = true;
            Debug.Log("Lock 3 set");
        }

        if (lock1 && lock2 && lock3)
        { 
            lock1 = false; lock2 = false; lock3 = false;
            return true;
        }

        return false;
    }
}

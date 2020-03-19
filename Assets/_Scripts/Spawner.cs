﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Spawns objects randomly in the player environment.
/// 
/// </summary>
public class Spawner : MonoBehaviour
{

    private float timeBetweenSpawns;
    public float startTimeBetweenSpawns;
    public float timeDecrease;
    public float minTime;
    public GameObject key;
    public GameObject[] obstacles;

    public static bool gesturePhase = false;

    private void Start()
    {
        timeBetweenSpawns = startTimeBetweenSpawns;
        key.SetActive(false);
    }

    private void Update()
    {
        if (!gesturePhase)
        {
            SpawnEnimies();
        }
        else
        {
            SpawnKey();
        }

    }

    public void SpawnKey()
    {
        key.SetActive(true);

        // Once finished set gesturephase to false
    }

    private void SpawnEnimies()
    {
        if (timeBetweenSpawns <= 0)
        {
            // Pick a random value depending on the amount of obstacles.
            int rand = Random.Range(0, obstacles.Length);
            // Instantiate a random amount of obstacles.
            Instantiate(obstacles[rand], transform.position, Quaternion.identity);
            // Reset the time between spawns
            timeBetweenSpawns = startTimeBetweenSpawns;
            if (startTimeBetweenSpawns > minTime)
            {
                startTimeBetweenSpawns -= timeDecrease;
            }
        }
        else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }

}

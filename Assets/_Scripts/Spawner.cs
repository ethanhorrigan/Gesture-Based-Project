using System.Collections;
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
    public bool spawnedKey = false;

    private void Start()
    {
        timeBetweenSpawns = startTimeBetweenSpawns;
    }

    private void Update()
    {
        if (!gesturePhase)
        {
            spawnedKey = false;
            SpawnEnimies();
        }
        else if (gesturePhase && !spawnedKey)
        {
            spawnedKey = true;
            SpawnGesturePhase();
        }
    }

    public void SpawnGesturePhase()
    {
        // Spawn in a password
        Instantiate(key, new Vector2(40, 2), Quaternion.identity);

        // Once finished set gesturephase to false
        //GesturePhaseOver();
    }

    private void GesturePhaseOver()
    {
        gesturePhase = false;
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

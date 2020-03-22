using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GesturePhase : MonoBehaviour
{
    // Import all gestures
    public GameObject[] gestures;
    // Gesture Phase Identifier

    //Spawn gesture
    private void SpawnGestures()
    {
        // pick a random gesture
        // spawn a gesture
        // ensure gesture phase lasts 5 iterations
        // if gesture reaches the scorehandler, end gesture phase, take life
        // if player gets all gestures correct, end gesture phase
        //if (timeBetweenSpawns <= 0)
        //{
        //    // Pick a random value depending on the amount of obstacles.
        //    int rand = Random.Range(0, obstacles.Length);
        //    // Instantiate a random amount of obstacles.
        //    Instantiate(obstacles[rand], transform.position, Quaternion.identity);
        //    // Reset the time between spawns
        //    timeBetweenSpawns = startTimeBetweenSpawns;
        //    if (startTimeBetweenSpawns > minTime)
        //    {
        //        startTimeBetweenSpawns -= timeDecrease;
        //    }
        //}
        //else
        //{
        //    timeBetweenSpawns -= Time.deltaTime;
        //}
    }

    // Translate Gesture

}

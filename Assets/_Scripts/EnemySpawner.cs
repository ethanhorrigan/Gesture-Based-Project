using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy Spawner Class.
/// Instantiates enemies in the prefab position
/// </summary>
public class EnemySpawner : MonoBehaviour
{

    /// <value>Enemy object for spawning</value>
    public GameObject enemy;
    private void Start()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    private void Start()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}

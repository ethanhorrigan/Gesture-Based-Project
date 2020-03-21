using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    /// <summary>
    /// 
    /// Enemy Death Handler ensures enemys have a "Life Span" so that enemey objects
    /// are destroyed after a peroid of time, to prevent stacking of unused objects.
    /// 
    /// </summary>
    /// 

    public float lifespan; //a float variable to set how long the enemies lifespan is.
    void Start()
    {

        Destroy(gameObject, lifespan);
    }

}

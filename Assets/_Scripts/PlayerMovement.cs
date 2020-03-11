using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject lanes;

    private Vector3 topLane, midLane, botLane;


    void Start()
    {
        player = GameObject.Find("Player");
        lanes = GameObject.Find("Lanes");

        SetLanes();

        player.transform.position = midLane;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GoUpLane();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            GoDownLane();
        }

    }

    private void GoDownLane()
    {
        if (player.transform.position == topLane)
        {
            player.transform.position = midLane;
        }
        else if (player.transform.position == midLane)
        {
            player.transform.position = botLane;
        }
        else
        {
            player.transform.position = botLane;
        }
    }

    private void GoUpLane()
    {
        if (player.transform.position == botLane)
        {
            player.transform.position = midLane;
        }
        else if (player.transform.position == midLane)
        {
            player.transform.position = topLane;
        }
        else
        {
            player.transform.position = topLane;
        }
    }

    void SetLanes()
    {
        Vector3 topLane = lanes.transform.Find("LaneTop").gameObject.transform.position;
        Vector3 midLane = lanes.transform.Find("LaneMiddle").gameObject.transform.position;
        Vector3 botLane = lanes.transform.Find("LaneBottom").gameObject.transform.position;
    }



}

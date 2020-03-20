using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject lanes;

    private GameObject topLane, midLane, botLane;
    public Animator cameraAnim;


    void Start()
    {
        player = GameObject.Find("Player");
        lanes = GameObject.Find("Lanes");

        SetLanes();

        player.transform.position = midLane.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Go UP");
            cameraAnim.SetTrigger("shake");
            GoUpLane();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Go DOWN");
            cameraAnim.SetTrigger("shake");
            GoDownLane();
        }

    }

    private void GoDownLane()
    {
        if (player.transform.position == topLane.transform.position)
        {
            player.transform.position = midLane.transform.position;
        }
        else if (player.transform.position == midLane.transform.position)
        {
            player.transform.position = botLane.transform.position;
        }
        else
        {
            player.transform.position = botLane.transform.position;
        }
    }

    private void GoUpLane()
    {
        if (player.transform.position == botLane.transform.position)
        {
            player.transform.position = midLane.transform.position;
        }
        else if (player.transform.position == midLane.transform.position)
        {
            player.transform.position = topLane.transform.position;
        }
        else
        {
            player.transform.position = topLane.transform.position;
        }
    }

    void SetLanes()
    {
        topLane = lanes.transform.Find("LaneTop").gameObject;
        midLane = lanes.transform.Find("LaneMiddle").gameObject;
        botLane = lanes.transform.Find("LaneBottom").gameObject;
    }



}

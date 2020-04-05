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

    public GameObject playerPFX;

    public AudioClip jumpSFX;
    public AudioClip hurtSound;
    AudioSource audioSource;

    void Start()
    {
        //get the audio source component
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        lanes = GameObject.Find("Lanes");

        SetLanes();

        player.transform.position = midLane.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            cameraAnim.SetTrigger("shake");
            audioSource.PlayOneShot(jumpSFX);
            GoUpLane();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            cameraAnim.SetTrigger("shake");
            audioSource.PlayOneShot(jumpSFX);
            GoDownLane();
        }
    }
    private void GoDownLane()
    {
        if (player.transform.position == topLane.transform.position)
        {
            player.transform.position = midLane.transform.position;
            Instantiate(playerPFX, midLane.transform.position, Quaternion.identity);
        }
        else if (player.transform.position == midLane.transform.position)
        {
            player.transform.position = botLane.transform.position;
            Instantiate(playerPFX, botLane.transform.position, Quaternion.identity);
        }
        else
        {
            player.transform.position = botLane.transform.position;
            Instantiate(playerPFX, botLane.transform.position, Quaternion.identity);
        }
    }

    private void GoUpLane()
    {
        if (player.transform.position == botLane.transform.position)
        {
            player.transform.position = midLane.transform.position;
            Instantiate(playerPFX, midLane.transform.position, Quaternion.identity);
        }
        else if (player.transform.position == midLane.transform.position)
        {
            player.transform.position = topLane.transform.position;
            Instantiate(playerPFX, topLane.transform.position, Quaternion.identity);
        }
        else
        {
            player.transform.position = topLane.transform.position;
            Instantiate(playerPFX, topLane.transform.position, Quaternion.identity);
        }
    }

    void SetLanes()
    {
        topLane = lanes.transform.Find("LaneTop").gameObject;
        midLane = lanes.transform.Find("LaneMiddle").gameObject;
        botLane = lanes.transform.Find("LaneBottom").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.PlayOneShot(hurtSound);
    }

}

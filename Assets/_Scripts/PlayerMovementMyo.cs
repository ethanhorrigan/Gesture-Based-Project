using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using System;

// Controlls what lane the player is in
// Send haptic feedback when a movement is made
public class PlayerMovementMyo : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    private GameObject player;
    private GameObject lanes;


    private GameObject topLane, midLane, botLane;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    public GameObject playerPFX;
    public Animator cameraAnim;
    public AudioClip jumpSFX;
    public AudioClip hurtSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        lanes = GameObject.Find("Lanes");

        SetLanes();

        player.transform.position = midLane.transform.position;

    }


    // Update is called once per frame.
    void Update()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;


            if (thalmicMyo.pose == Pose.WaveIn)
            {
                cameraAnim.SetTrigger("shake");
                audioSource.PlayOneShot(jumpSFX);
                GoDownLane();

                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.WaveOut)
            {
                cameraAnim.SetTrigger("shake");
                audioSource.PlayOneShot(jumpSFX);
                GoUpLane();

                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
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

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }

    
}

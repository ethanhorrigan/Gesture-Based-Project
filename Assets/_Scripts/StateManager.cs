using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject spawner;
    public GameObject playerObject;

    public Text scoreText;
    public float timer;
    public int score;

    private bool paused = false;


    public Player player;

 
    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    private void Start()
    {
    }


    // Update is called once per frame.
    void Update()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        if (player.health <= 0)
        {
            spawner.SetActive(false);
            deathMenu.gameObject.SetActive(true);
            Destroy(playerObject);
        }

        ScoreIncrease();

        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;


            
        }

        if (thalmicMyo.pose == Pose.DoubleTap || Input.GetKeyDown(KeyCode.T))
        {
            thalmicMyo.Vibrate(VibrationType.Medium);

            if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
            }

            ExtendUnlockAndNotifyUserAction(thalmicMyo);
        }

        if (Input.GetKeyDown(KeyCode.R) || thalmicMyo.pose == Pose.FingersSpread && player.health == 0)
        {
            Debug.Log("fingers spread");
            SceneManager.LoadSceneAsync("SpawnTest", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                paused = false;
                pauseMenu.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(true);
                spawner.SetActive(true);
                playerObject.SetActive(true);
            }
            else
            {
                paused = true;
                pauseMenu.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(false);
                spawner.SetActive(false);
                playerObject.SetActive(false);
            }
        }

        

    }

    private void ScoreIncrease()
    { 
        
        if (!paused)
        {
            timer += Time.deltaTime;
            if (timer > 5f)
            {

                score += 5;

                //We only need to update the text if the score changed.
                scoreText.text = score.ToString();

                //Reset the timer to 0.
                timer = 0;
            }
        }
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

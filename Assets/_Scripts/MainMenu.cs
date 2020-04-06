using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using UnityEngine.SceneManagement;
public class MainMenu
{
    //public GameObject myo = null;

    //private Pose _lastPose = Pose.Unknown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        /*
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;
        }
        */

        if (Input.GetKeyDown(KeyCode.Space) /*|| thalmicMyo.pose == Pose.FingersSpread*/)
        {
            SceneManager.LoadSceneAsync("SpawnTest");
        }
    }
    /*
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
    */
}

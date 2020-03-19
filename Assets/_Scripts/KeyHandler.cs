using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class KeyHandler : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    // Values to correspond with Pose Images.
    private int[] PoseValues;
    private Pose[] poses;

    public static int[] KeyOrder;

    private bool[] locks;

    // An array of images of myo poses.
    public Sprite[] PoseImages;

    // Start is called before the first frame update
    void Start()
    {
        PoseValues = new int[Random.Range(2,6)];
        KeyOrder = new int[PoseValues.Length];
        locks = new bool[PoseValues.Length];

        for(int i = 0; i < PoseValues.Length; i++)
        {
            PoseValues[i] = Random.Range(1, 6);

            switch (PoseValues[i])
            {
                case 1:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.WAVE_LEFT];
                    KeyOrder[i] = Constants.WAVE_LEFT;
                    break;
                case 2:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.WAVE_RIGHT];
                    KeyOrder[i] = Constants.WAVE_RIGHT;
                    break;
                case 3:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.DOUBLE_TAP];
                    KeyOrder[i] = Constants.DOUBLE_TAP;
                    break;
                case 4:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.FIST];
                    KeyOrder[i] = Constants.FIST;
                    break;
                case 5:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.SPREAD];
                    KeyOrder[i] = Constants.SPREAD;
                    break;

            }
        }

        poses = CreatePassword(KeyOrder);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        
        // Look for myo poses 
    }

    // Creates an array of poses used as a password.
    private Pose[] CreatePassword(int[] passwordOrder)
    {
        for(int i = 0; i < locks.Length; i++)
        {
            locks[i] = false;
        }

        Pose[] passwordPoses;

        passwordPoses = new Pose[passwordOrder.Length];

        for(int i = 0; i < passwordOrder.Length; i++)
        {
            switch (passwordOrder[i])
            {
                case 0:
                    passwordPoses[i] = Pose.WaveIn;
                    break;
                case 1:
                    passwordPoses[i] = Pose.WaveOut;
                    break;
                case 2:
                    passwordPoses[i] = Pose.DoubleTap;
                    break;
                case 3:
                    passwordPoses[i] = Pose.Fist;
                    break;
                case 4:
                    passwordPoses[i] = Pose.FingersSpread;
                    break;
            }
        }

        return passwordPoses;
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

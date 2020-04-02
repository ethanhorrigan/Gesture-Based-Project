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
    private GameObject myo = null;
    private GameObject hub;

    private Transform[] lockTransforms;
    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    // Values to correspond with Pose Images.
    private int[] PoseLength;
    private Pose[] poses;

    public static int[] KeyOrder;

    private float speed = 4.0f;

    // An array of images of myo poses.
    public Sprite[] PoseImages;

    private int correctGestureCount = 0;

    public static bool endPhase = false;

    // Start is called before the first frame update
    void Start()
    {
        hub = GameObject.Find("Hub - 1 Myo").gameObject;
        myo = hub.transform.Find("Myo").gameObject;

        PoseLength = new int[Random.Range(2,6)];
        poses = new Pose[PoseLength.Length];

        lockTransforms = GetComponentsInChildren<Transform>();

        for (int i = 0; i < PoseLength.Length; i++)
        {
            PoseLength[i] = Random.Range(1, 6);

            switch (PoseLength[i])
            {
                case 1:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.WAVE_LEFT];
                    poses[i] = Pose.WaveIn;
                    break;
                case 2:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.WAVE_RIGHT];
                    poses[i] = Pose.WaveOut;
                    break;
                case 3:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.DOUBLE_TAP];
                    poses[i] = Pose.DoubleTap;
                    break;
                case 4:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.FIST];
                    poses[i] = Pose.Fist;
                    break;
                case 5:
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = PoseImages[Constants.SPREAD];
                    poses[i] = Pose.FingersSpread;
                    break;

            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        // Move the password
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        TryPassword(poses);

        // Check if all the locks are true, if they are the gesture phase is ended.
        if (correctGestureCount == poses.Length)
        {
            Spawner.gesturePhase = false;
            Debug.Log("All locks true");
            Destroy(this);
        }

        if (ScoreHandler.gestureCount > 0)
        {
            transform.gameObject.SetActive(false);
            ScoreHandler.gestureCount = 0;
        }
    }

    // Takes in password and locks, checks to see if the user's pose matches the pose in the password array
    // before moving onto the next gesture.
    private void TryPassword(Pose[] poses)
    {
        int currentPose = 0;
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        // Don't move onto the next lock until the current one has been unlocked.
        if (thalmicMyo.pose == poses[currentPose])
        {
            lockTransforms[currentPose].gameObject.SetActive(false);
            currentPose++;
            correctGestureCount++;
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

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

    public static int[] KeyOrder;

    // An array of images of myo poses.
    public Sprite[] PoseImages;

    // Start is called before the first frame update
    void Start()
    {
        PoseValues = new int[Random.Range(2,6)];
        KeyOrder = new int[PoseValues.Length];

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
        Debug.Log("Printing out key order");
        for(int i = 0; i < KeyOrder.Length; i++)
        {
            Debug.Log(KeyOrder[i]);
        }
        Debug.Log("printed out key order");
        Debug.Log(PoseValues.Length.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
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

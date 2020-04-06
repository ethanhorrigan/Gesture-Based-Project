using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

/**
 * 
 * Statemanager class handles the current state of the game.
 * These states include, Play State, Pause State and Death State.
 *
 * Pausing and Continueing a game is done through speech.
 * Adapted from: https://docs.unity3d.com/ScriptReference/Windows.Speech.KeywordRecognizer.html
 * 
 * Authors: 
 * Ethan Horrigan
 * Dylan Loftus
 * 
 */
public class StateManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject spawner;
    public GameObject playerObject;

    private bool inPause = false;
    public float timer;

    private int nextGesturePhase = 4;

    public static bool paused = false;
    public Player player;
    public int pauseTimer;

    /* Speech Recognition Variables */
    protected PhraseRecognizer recognizer;
    public string[] keywords = new string[] { "pause", "play", "continue", "quit" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    protected string word;
    /* End of Speech Recognition Variables */


    void Start()
    {
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }
    }
    void Update()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        if (player.health <= 0)
        {
            spawner.SetActive(false);
            deathMenu.gameObject.SetActive(true);
            Destroy(playerObject);
        }

        switch(word)
        {
            case "pause":
                if (!inPause)
                {
                    paused = true;
                    inPause = true;
                    BackgroundHandler.moving = false;
                    EnemyHandler.moving = false;
                    spawner.SetActive(false);
                    playerObject.SetActive(false);
                    pauseMenu.gameObject.SetActive(true);
                }
                break;
            case "continue":
                if(inPause)
                {
                    paused = false;
                    inPause = false;
                    BackgroundHandler.moving = true;
                    EnemyHandler.moving = true;
                    pauseMenu.gameObject.SetActive(false);
                    spawner.SetActive(true);
                    playerObject.SetActive(true);
                }
                break;
            case "play":
                if(player.health == 0)
                    SceneManager.LoadSceneAsync("SpawnTest", LoadSceneMode.Single);
                break;
            case "quit":
                break;
        }

        if (Input.GetKeyDown(KeyCode.R) && player.health == 0)
        {
            Debug.Log("fingers spread");
            SceneManager.LoadSceneAsync("SpawnTest", LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (pauseMenu.gameObject.activeSelf)
            {
                paused = false;
                BackgroundHandler.moving = true;
                EnemyHandler.moving = true;
                pauseMenu.gameObject.SetActive(false);
                spawner.SetActive(true);
                playerObject.SetActive(true);
            }
            else
            {
                paused = true;
                BackgroundHandler.moving = false;
                EnemyHandler.moving = false;
                spawner.SetActive(false);
                playerObject.SetActive(false);
                pauseMenu.gameObject.SetActive(true);
            }
        }
        
        if(ScoreHandler.score > nextGesturePhase)
        {
            nextGesturePhase += 4;
            Spawner.gesturePhase = true;
        }
        
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        Debug.Log(word);
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Score Handler Class.
/// Contains all methods for handling score.
/// Each enemy that triggers the score hanlder object
/// will increase the score.
/// </summary>
/// <remarks>
/// This way of managing the score feels better than the previous.
/// Instead of score being based off time, score is based off the amount of
/// enemies that the player has passed.
/// </remarks>
public class ScoreHandler : MonoBehaviour
{
    public Text scoreText;
    public static int score = 0;
    public static int gestureCount = 0;
    public AudioClip hurtSound;
    AudioSource audioSource;
    private GameObject player;


    /// <summary>
    /// Retrieves the Audio Source Component
    /// and the Player Object
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// Inreases the players score each time 
    /// an enemy triggers the scorehandler collider.
    /// </summary>
    /// <param name="other">The other collided the score handler has collided with.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!StateManager.paused)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
                score++;
                scoreText.text = score.ToString();
            }
            else
            {
                audioSource.PlayOneShot(hurtSound);
                Destroy(other.gameObject);
                gestureCount++;
                Spawner.gesturePhase = false;
                player.GetComponent<Player>().health--;
            }

        }
    }
}

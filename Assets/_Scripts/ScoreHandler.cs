using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public Text scoreText;
    public static int score = 0;
    public static int gestureCount = 0;
    public AudioClip hurtSound;
    AudioSource audioSource;
    private GameObject player;
    

    /**
     * Added an Score Handler through a Trigger, So each enemy that is passed,
     * the score will increase.
     * 
     * This way of managing the score feels better than the previous.
     * Instead of score being based off time, score is based off the amount of
     * enemies that the player has passed.
     * 
    */

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!StateManager.paused)
        {
            if(other.gameObject.tag == "Enemy")
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    /**
     * Added an Score Handler through a Trigger, So each enemy that is passed,
     * the score will increase.
     * 
     * This way of managing the score feels better than the previous.
     * Instead of score being based off time, score is based off the amount of
     * enemies that the player has passed.
     * 
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!StateManager.paused)
        {
            
            Destroy(other.gameObject);
            score++;
            scoreText.text = score.ToString();
        }
    }
}

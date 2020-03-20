using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!StateManager.paused)
        {
            score++;
            Destroy(other.gameObject);
            scoreText.text = score.ToString();

        }
    }
}

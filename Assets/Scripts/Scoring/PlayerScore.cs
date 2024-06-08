using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;


    public int CurrentScore { get; private set; }

    public void AddScore(int score = 1)
    {
        CurrentScore += score;
        _scoreText.text = $"SCORE: {CurrentScore}";

        // Very dirty way of checking the score and loading the game over scene
        if(CurrentScore == 3)
        {
            SceneManager.LoadScene(1);
        }
    }
}

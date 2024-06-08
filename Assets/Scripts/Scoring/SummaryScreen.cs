using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SummaryScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private void Start()
    {
        UpdateSummary();
    }

    public void PlayAgain()
    {
        // Load the game scene
        SceneManager.LoadScene(0);
    }

    public void UpdateSummary()
    {
        _scoreText.text = $"Your Score {ScoreManager.Instance.CurrentScore}";
    }
}

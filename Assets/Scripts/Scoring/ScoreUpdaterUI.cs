using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreUpdaterUI : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();   
    }
    private void Update()
    {
        _scoreText.text = $"SCORE: {ScoreManager.Instance.CurrentScore}";
    }
}

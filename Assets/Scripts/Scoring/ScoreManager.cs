using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    // Singleton implementation
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            // if the instance doesnt have a reference yet,
            if( _instance == null)
            {
                // Try finding a game object in the scene that has the script attached to it
                _instance = FindObjectOfType<ScoreManager>();
                // Add another layer of checking when there is no gameobject with the script attached at the start
                if( _instance == null ) 
                {
                    // Create a new gameobject and add the script
                    GameObject gameObj = new();
                    gameObj.name = "ScoreManager";
                    // Make sure to set the instance as the created object
                    _instance = gameObj.AddComponent<ScoreManager>();
                    // Make sure the instance does not get destroyed when loading new scenes
                    DontDestroyOnLoad(gameObj);
                }
            }
            return _instance;
        }
    }


    // Perform the actual "single instance" only behavior
    private void Awake()
    {
        // IF there's no Instance, set this as the instance
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // There is a duplicate of the script
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public int CurrentScore { get; private set; }

    public void AddScore(int score = 1)
    {
        CurrentScore += score;
        // Very dirty way of checking the score and loading the game over scene
        if (CurrentScore == 3)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }
}

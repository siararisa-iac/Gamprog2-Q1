using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    /*
    // Get a reference to the player score script so we can add score
    private PlayerScore _playerScore;

    private void Start()
    {
        // Make sure we get the playerscore component reference
        _playerScore = GetComponent<PlayerScore>();
    }*/

    
    private void Start()
    {
        ScoreManager.Instance.ResetScore();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Collectible")
        {
            ScoreManager.Instance.AddScore();
            // Make sure to destroy the object when collided
            Destroy(collider.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Games.Relay;
using UnityEngine;

public class RelayGameController : MonoBehaviour
{
    [SerializeField] public RelayPlayerController[] players;
    [SerializeField] public int currentPlayerIndex = 0;
    
    private void Start()
    {
        currentPlayerIndex = 0;
        players[currentPlayerIndex].isTurn = true;
    }

    private void Update()
    {
        if (players[currentPlayerIndex].progress > 900)
        {
            if (currentPlayerIndex == players.Length - 1)
            {
                Debug.Log("Game Over");
                return;
            }
            else
            {
                // Debug.Log("Next Player's Turn"); 
                players[currentPlayerIndex + 1].transform.Translate(Vector3.left * (Time.deltaTime * 2));
            }
        }
        else
        {
            // Debug.Log("Current Player's Turn");
        }
    }
}

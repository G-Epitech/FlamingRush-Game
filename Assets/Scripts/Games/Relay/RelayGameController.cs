using System;
using System.Collections;
using System.Collections.Generic;
using Games.Relay;
using UnityEngine;

public class State
{
    public string type;
    public Dictionary<string, int> runners;
    public int currentRunner;
    public Dictionary<string, string> obstacles;
    public int remainingTime;
    public int distance;
    public bool transitionZone;
}

public class RelayGameController : MonoBehaviour
{
    [SerializeField] public RelayPlayerController[] players;
    [SerializeField] public int currentPlayerIndex = 0;
    public GameManager gameManager { get; private set; }

    public State State;

    private void OnDestroy()
    {
        gameManager?.client.Off("games/state");
    }

    private void UpdateState(State state)
    {
        State = state;
    }

    private void Start()
    {
        currentPlayerIndex = 0;
        players[currentPlayerIndex].isTurn = true;
        gameManager = FindObjectOfType<GameManager>();
        gameManager?.client.OnUnityThread("games/state", response => UpdateState(response.GetValue<State>()));
    }

    private void Update()
    {
        Sync();
        if (State.transitionZone)
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

    private void Sync()
    {
        if (currentPlayerIndex == State.currentRunner)
            return;
        players[currentPlayerIndex].isFinished = true;
        players[currentPlayerIndex].isTurn = false;
        currentPlayerIndex = State.currentRunner;
        players[currentPlayerIndex].isTurn = true;
        players[currentPlayerIndex].PreventMisplacement();
    }
    
    public void EmitPass()
    {
        gameManager?.client.Emit("games/relay/pass");
    }
    
    public void EmitTap()
    {
        gameManager?.client.Emit("games/relay/tap");
    }
}
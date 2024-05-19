using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ReturnMenuController : MonoBehaviour
{
    [SerializeField] private Fade fade;
    
    public void ReturnMenu()
    {
        var gameManager = GameObject.FindObjectOfType<GameManager>();
        
        gameManager.gameData.start = false;
        gameManager.gameData.lifes = 3;
        gameManager.gameData.streak = 0;
        fade.FadeIn("MainMenu");
    }
}

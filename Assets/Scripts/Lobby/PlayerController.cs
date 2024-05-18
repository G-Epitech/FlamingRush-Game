using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby {

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite youSprite;
    [SerializeField] private Sprite noPlayerSprite;
    [SerializeField] private Sprite playerTitleSprite;
    [SerializeField] private Sprite playerReadySprite;
    [SerializeField] private Sprite playerNotReadySprite;
    [SerializeField] private Image player1SpriteRenderer;
    [SerializeField] private Image player2SpriteRenderer;
    [SerializeField] private Image player3SpriteRenderer;
    [SerializeField] private Image player4SpriteRenderer;
    [SerializeField] private TextMeshProUGUI player1Text;
    [SerializeField] private TextMeshProUGUI player2Text;
    [SerializeField] private TextMeshProUGUI player3Text;
    [SerializeField] private TextMeshProUGUI player4Text;
    [SerializeField] private Image player1YouSpriteRenderer;
    [SerializeField] private Image player2YouSpriteRenderer;
    [SerializeField] private Image player3YouSpriteRenderer;
    [SerializeField] private Image player4YouSpriteRenderer;

    private void SetPlayer(int playerNumber, string playerName, bool isYou, bool isReady)
    {
        Image spriteRenderer = null;
        TextMeshProUGUI text = null;
        Image youSpriteRenderer = null;
        
        switch (playerNumber)
        {
            case 1:
                spriteRenderer = player1SpriteRenderer;
                text = player1Text;
                youSpriteRenderer = player1YouSpriteRenderer;
                break;
            case 2:
                spriteRenderer = player2SpriteRenderer;
                text = player2Text;
                youSpriteRenderer = player2YouSpriteRenderer;
                break;
            case 3:
                spriteRenderer = player3SpriteRenderer;
                text = player3Text;
                youSpriteRenderer = player3YouSpriteRenderer;
                break;
            case 4:
                spriteRenderer = player4SpriteRenderer;
                text = player4Text;
                youSpriteRenderer = player4YouSpriteRenderer;
                break;
        }

        if (isYou)
        {
            youSpriteRenderer.sprite = youSprite;
            youSpriteRenderer.color = Color.white;
        }
        else
        {
            youSpriteRenderer.sprite = noPlayerSprite;
            youSpriteRenderer.color = Color.clear;
        }

        if (playerName == "")
        {
            spriteRenderer.sprite = noPlayerSprite;
            spriteRenderer.color = Color.clear;
            text.text = "";
        }
        else
        {
            spriteRenderer.sprite = playerTitleSprite;
            spriteRenderer.color = Color.white;
            text.text = playerName;
        }

        if (isReady)
        {
            spriteRenderer.sprite = playerReadySprite;
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.sprite = playerNotReadySprite;
            spriteRenderer.color = Color.white;
        }
    }

    public void Start()
    {
        SetPlayer(1, "HUGO", false, false);
        SetPlayer(2, "MATH", true, false);
        SetPlayer(3, "YANN", false, false);
        SetPlayer(4, "DRAG", false, false);
    }
}

}
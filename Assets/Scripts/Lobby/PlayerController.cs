using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Lobby {

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite youSprite;
    [SerializeField] private Sprite noPlayerSprite;
    [SerializeField] private Sprite playerTitleSprite;
    [SerializeField] private Sprite playerReadySprite;
    [SerializeField] private Sprite playerNotReadySprite;
    [SerializeField] private SpriteRenderer player1SpriteRenderer;
    [SerializeField] private SpriteRenderer player2SpriteRenderer;
    [SerializeField] private SpriteRenderer player3SpriteRenderer;
    [SerializeField] private SpriteRenderer player4SpriteRenderer;
    [SerializeField] private TextMeshProUGUI player1Text;
    [SerializeField] private TextMeshProUGUI player2Text;
    [SerializeField] private TextMeshProUGUI player3Text;
    [SerializeField] private TextMeshProUGUI player4Text;
    [SerializeField] private SpriteRenderer player1YouSpriteRenderer;
    [SerializeField] private SpriteRenderer player2YouSpriteRenderer;
    [SerializeField] private SpriteRenderer player3YouSpriteRenderer;
    [SerializeField] private SpriteRenderer player4YouSpriteRenderer;

    private void SetPlayer(int playerNumber, string playerName, bool isYou, bool isReady)
    {
        SpriteRenderer spriteRenderer = null;
        TextMeshProUGUI text = null;
        SpriteRenderer youSpriteRenderer = null;
        
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
        }
        else
        {
            youSpriteRenderer.sprite = noPlayerSprite;
        }

        if (playerName == "")
        {
            spriteRenderer.sprite = noPlayerSprite;
            text.text = "";
        }
        else
        {
            spriteRenderer.sprite = playerTitleSprite;
            text.text = playerName;
        }

        if (isReady)
        {
            spriteRenderer.sprite = playerReadySprite;
        }
        else
        {
            spriteRenderer.sprite = playerNotReadySprite;
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
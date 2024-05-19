using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private string profilesPicturesPath;
        [SerializeField] private Sprite youSprite;
        [SerializeField] private Sprite noSprite;
        [SerializeField] private Sprite noPlayerTitleSprite;
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
        [SerializeField] private Image player1ProfileRenderer;
        [SerializeField] private Image player2ProfileRenderer;
        [SerializeField] private Image player3ProfileRenderer;
        [SerializeField] private Image player4ProfileRenderer;
        [SerializeField] private TextMeshProUGUI codeText;

        public void SetPlayer(int playerNumber, string playerName, int playerProfileIdx, bool isYou, bool isReady)
        {
            List<Sprite> images = ImageLoader.loadImagesFromPath(profilesPicturesPath);
            Image spriteRenderer = null;
            Image profileRenderer = null;
            TextMeshProUGUI text = null;
            Image youSpriteRenderer = null;

            switch (playerNumber)
            {
                case 1:
                    spriteRenderer = player1SpriteRenderer;
                    text = player1Text;
                    youSpriteRenderer = player1YouSpriteRenderer;
                    profileRenderer = player1ProfileRenderer;
                    break;
                case 2:
                    spriteRenderer = player2SpriteRenderer;
                    text = player2Text;
                    youSpriteRenderer = player2YouSpriteRenderer;
                    profileRenderer = player2ProfileRenderer;
                    break;
                case 3:
                    spriteRenderer = player3SpriteRenderer;
                    text = player3Text;
                    youSpriteRenderer = player3YouSpriteRenderer;
                    profileRenderer = player3ProfileRenderer;
                    break;
                case 4:
                    spriteRenderer = player4SpriteRenderer;
                    text = player4Text;
                    youSpriteRenderer = player4YouSpriteRenderer;
                    profileRenderer = player4ProfileRenderer;
                    break;
            }

            profileRenderer.gameObject.SetActive(true);
            profileRenderer.sprite = images[playerProfileIdx];

            if (isYou)
            {
                youSpriteRenderer.gameObject.SetActive(true);
            }

            if (playerName == "")
            {
                spriteRenderer.sprite = noSprite;
                spriteRenderer.color = Color.clear;
                text.text = "";
            }
            else
            {
                spriteRenderer.sprite = playerTitleSprite;
                spriteRenderer.color = Color.white;
                text.text = playerName;
            }

            if (isReady && playerNumber != 1)
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

        public void SetRoomCode(string code)
        {
            codeText.text = code;
        }

        private void Init()
        {
            player2SpriteRenderer.sprite = noPlayerTitleSprite;
            player3SpriteRenderer.sprite = noPlayerTitleSprite;
            player4SpriteRenderer.sprite = noPlayerTitleSprite;

            player1ProfileRenderer.gameObject.SetActive(false);
            player2ProfileRenderer.gameObject.SetActive(false);
            player3ProfileRenderer.gameObject.SetActive(false);
            player4ProfileRenderer.gameObject.SetActive(false);
            
            player1YouSpriteRenderer.gameObject.SetActive(false);
            player2YouSpriteRenderer.gameObject.SetActive(false);
            player3YouSpriteRenderer.gameObject.SetActive(false);
            player4YouSpriteRenderer.gameObject.SetActive(false);
        }

        public void Start()
        {
            Init();

            var gameManagerObject = GameObject.FindWithTag("GameManager");
            if (gameManagerObject)
            {
                GameManager gameManager = gameManagerObject.GetComponent<GameManager>();

                gameManager.askRoomStatus();
            }
        }
    }
}
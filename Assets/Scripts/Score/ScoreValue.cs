using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreValue : MonoBehaviour
{
    [SerializeField] private Image _leftDigit;
    [SerializeField] private Image _rightDigit;
    private List<Sprite> _digitSprites;
    private string _digitPath = "Flame Interlude/";

    private void Start()
    {
        _digitSprites = new List<Sprite>();
        LoadDigitSprites();
        UpdateScoreImages();
    }

    private void LoadDigitSprites()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(_digitPath);
        if (sprites != null && sprites.Length > 0)
        {
            _digitSprites.AddRange(sprites);
        }
        else
        {
            Debug.LogWarning("No sprites found in the specified texture.");
        }
    }

    private void UpdateScoreImages()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.gameData.streak <= 9)
        {
            _leftDigit.sprite = _digitSprites[0];
            _rightDigit.sprite = _digitSprites[gameManager.gameData.streak];
            return;
        }
        int firstDigit = gameManager.gameData.streak;
        while (firstDigit >= 10)
        {
            firstDigit /= 10;
        }
        _leftDigit.sprite = _digitSprites[firstDigit];
        _rightDigit.sprite = _digitSprites[gameManager.gameData.streak % 10];
    }
}

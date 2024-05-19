using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameManager : MonoBehaviour
{
    [SerializeField] private Image flame;
    [SerializeField] private Sprite[] flameSprites;
    [SerializeField] private Image[] lifes;
    [SerializeField] private Sprite[] lifesSprites;
    [SerializeField] private Image streak;
    [SerializeField] private Sprite[] streakSprites;
    [SerializeField] private Image vignette;
    [SerializeField] private Sprite[] vignetteSprites;
    [SerializeField] private Image effect;
    [SerializeField] private ParticleSystem fire;

    void Start()
    {
        var gameManager = GameObject.FindObjectOfType<GameManager>();
        uint lf = gameManager.gameData.lifes;
        
        var specialMoveBackground = GameObject.FindObjectOfType<SpecialMoveBackground>();

        specialMoveBackground.SetBackground(lf);
        flame.sprite = flameSprites[1];
        vignette.sprite = vignetteSprites[1];
        streak.sprite = streakSprites[lf];
        effect.gameObject.SetActive(true);
        fire.gameObject.SetActive(true);
        
        if (lf == 2)
        {
            effect.gameObject.SetActive(false);
            lifes[2].sprite = lifesSprites[0];
        }
        if (lf == 1)
        {
            effect.gameObject.SetActive(false);
            vignette.sprite = vignetteSprites[0];
            lifes[2].sprite = lifesSprites[0];
            lifes[1].sprite = lifesSprites[0];
        }
        if (lf == 0)
        {
            fire.gameObject.SetActive(false);
            effect.gameObject.SetActive(false);
            flame.sprite = flameSprites[0];
            vignette.sprite = vignetteSprites[0];
            lifes[0].sprite = lifesSprites[0];
            lifes[1].sprite = lifesSprites[0];
            lifes[2].sprite = lifesSprites[0];
        }
    }
}
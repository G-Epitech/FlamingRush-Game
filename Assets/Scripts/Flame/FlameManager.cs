using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flame;
    [SerializeField] private Sprite[] flameSprites;
    [SerializeField] private SpriteRenderer[] lifes;
    [SerializeField] private Sprite[] lifesSprites;
    [SerializeField] private SpriteRenderer streak;
    [SerializeField] private Sprite[] streakSprites;
    [SerializeField] private SpriteRenderer vignette;
    [SerializeField] private Sprite[] vignetteSprites;
    [SerializeField] private SpriteRenderer effect;
    [SerializeField] private ParticleSystem[] fire;

    void Start()
    {
        var gameManager = GameObject.FindObjectOfType<GameManager>();
        uint lf = gameManager.gameData.lifes;

        if (lf == 3)
        {
            SetView(lf);
            return;
        }

        SetView(lf + 1);
        StartCoroutine(LifeAnimation(lf + 1));
    }

    private IEnumerator LifeAnimation(uint lf)
    {
        Transform lifeTransform = lifes[lf - 1].transform;
        Vector3 originalScale = lifeTransform.localScale;
        Vector3 targetScale = new Vector3(124.6f, 124.6f, 124.6f);

        float elapsedTime = 0f;
        while (elapsedTime < 2)
        {
            lifeTransform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lifeTransform.localScale = targetScale;
        SetView(lf - 1);

        // Animate scaling down
        elapsedTime = 0f;
        while (elapsedTime < 2)
        {
            lifeTransform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lifeTransform.localScale = originalScale;
    }

    private void SetView(uint lf)
    {
        var specialMoveBackground = GameObject.FindObjectOfType<SpecialMoveBackground>();

        specialMoveBackground.SetBackground(lf);
        flame.sprite = flameSprites[1];
        vignette.sprite = vignetteSprites[1];
        streak.sprite = streakSprites[lf];
        effect.gameObject.SetActive(true);
        foreach (var system in fire)
        {
            system.gameObject.SetActive(true);
        }

        if (lf == 2)
        {
            effect.gameObject.SetActive(false);
            lifes[2].sprite = lifesSprites[0];
            
            foreach (var system in fire)
            {
                var emmision = system.emission;
                emmision.rateOverTime = 30.0f;
            }
        }

        if (lf == 1)
        {
            effect.gameObject.SetActive(false);
            vignette.sprite = vignetteSprites[0];
            lifes[2].sprite = lifesSprites[0];
            lifes[1].sprite = lifesSprites[0];
            
            foreach (var system in fire)
            {
                var emmision = system.emission;
                emmision.rateOverTime = 10.0f;
            }
        }

        if (lf == 0)
        {
            foreach (var system in fire)
            {
                system.gameObject.SetActive(false);
            }

            effect.gameObject.SetActive(false);
            flame.sprite = flameSprites[0];
            vignette.sprite = vignetteSprites[0];
            lifes[0].sprite = lifesSprites[0];
            lifes[1].sprite = lifesSprites[0];
            lifes[2].sprite = lifesSprites[0];
        }
    }

    private void Update()
    {
        if (effect.gameObject.activeSelf)
        {
            effect.transform.Rotate(0f, 0f, -0.35f * (Time.deltaTime * 360));
        }

        if (vignette.sprite == vignetteSprites[0])
        {
            float alpha = Mathf.PingPong(Time.time * 2.5f, 1.0f - 0.1f) + 0.1f;
            Color color = vignette.color;
            color.a = alpha;
            vignette.color = color;
        }
    }
}
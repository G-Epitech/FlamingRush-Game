using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

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
    [SerializeField] private Fade fade;

    void Start()
    {
        var gameManager = GameObject.FindObjectOfType<GameManager>();
        int lf = gameManager.gameData.lifes;

        Debug.Log("Flame receive " + lf + " lives");
        if (lf == 3)
        {
            SetView(lf);
        }
        else
        {
            SetView(lf + 1);
        }

        StartCoroutine(LifeAnimation(lf + 1, gameManager));
    }

    private IEnumerator LifeAnimation(int lf, GameManager gm)
    {
        if (lf > 3)
        {
            Debug.Log("No life changes");
            yield return new WaitForSeconds(4);
            gm.setReady();
            yield return null;
        }

        if (lf <= 3)
        {
            Debug.Log("Transition life");
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

            yield return new WaitForSeconds(2);
            gm.setReady();

            // End of the game
            if (lf - 1 <= 0)
            {
                fade.FadeIn("Score");
            }
        }
    }

    private void SetView(int lf)
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
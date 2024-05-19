using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utils
{
    public class Fade : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
            if (gameObject.scene.name == "Canoe")
                this.FadeOut();
            if (gameObject.scene.name == "FlameScore")
                this.FadeOut();
            if (gameObject.scene.name == "Score")
                this.FadeOut();
            if (gameObject.scene.name == "MainMenu")
                this.FadeOut();
        }

        private IEnumerator _fadeIn(string scene)
        {
            var image = gameObject.GetComponent<Image>();
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                image.color = new Color(0, 0, 0, i);
                yield return null;
            }

            SceneManager.LoadScene(scene);
        }

        public void FadeIn(string scene)
        {
            gameObject.SetActive(true);
            StartCoroutine(_fadeIn(scene));
        }

        private IEnumerator _fadeOut()
        {
            var image = gameObject.GetComponent<Image>();
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                image.color = new Color(0, 0, 0, i);
                yield return null;
            }
            gameObject.SetActive(false);
        }

        public void FadeOut()
        {
            gameObject.SetActive(true);
            StartCoroutine(_fadeOut());
        }
    }
}
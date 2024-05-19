using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Games.Canoe
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] public Sprite BigWaveSprite;
        [SerializeField] public Sprite SmallWaveSprite;
        [SerializeField] public Vector2[] wavePositions = new Vector2[] { new Vector2(0, 0), new Vector2(0, 0) };
        [SerializeField] public Vector2[] waveSizes = new Vector2[] { new Vector2(1, 1), new Vector2(1, 1) };

        private void GenerateWave(Vector2 position, Sprite sprite, Vector2 size)
        {
            var wave = new GameObject();
            wave.transform.name = "Wave";
            wave.AddComponent<Image>().sprite = sprite;
            var mainCanvas = GameObject.Find("Waves");
            wave.transform.SetParent(mainCanvas.transform);
            Debug.Log("Wave generated at " + position);
            wave.transform.localScale = new Vector3(1, 1, 1);
            var rect = wave.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            wave.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, size.y);
            wave.AddComponent<WaveMove>();
        }

        private void Start()
        {
            // Start the coroutine to generate waves at random intervals
            StartCoroutine(GenerateWavesContinuously());
        }

        private IEnumerator GenerateWavesContinuously()
        {
            while (true)
            {
                // Randomize position, size, and interval
                var randomIndex = Random.Range(0, 2);
                Vector2 randomPosition = new Vector2(2000, Random.Range(-240, 120));
                Vector2 randomSize = waveSizes[randomIndex];
                Sprite randomSprite = randomIndex == 0 ? BigWaveSprite : SmallWaveSprite;
                float randomInterval = Random.Range(4f, 6f); // Generate waves between 4 to 6 seconds

                // Generate the wave
                GenerateWave(randomPosition, randomSprite, randomSize);

                // Wait for a random interval before generating the next wave
                yield return new WaitForSeconds(randomInterval);
            }
        }
    }
}

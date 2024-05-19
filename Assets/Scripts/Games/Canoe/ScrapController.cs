using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Games.Canoe
{
    public class ScrapController : MonoBehaviour
    {
        [SerializeField] public float waveSpeed = 2f;
        [SerializeField] public Sprite[] sprites;
        [SerializeField] public Vector2[] scrapSizes;

        private Vector2[] _scrapPositions = new Vector2[4]
        {
            new Vector2(2000, 250), new Vector2(2000, 75), new Vector2(2000, -90), new Vector2(2000, -280)
        };

        private void GenerateScrap(Vector2 position, Sprite sprite, Vector2 size, string uuid)
        {
            var scrap = new GameObject();
            scrap.transform.name = "Scrap";
            scrap.AddComponent<Image>().sprite = sprite;
            var mainCanvas = GameObject.Find("Scraps");
            scrap.transform.SetParent(mainCanvas.transform);
            Debug.Log("Scrap generated at " + position);
            scrap.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            var rect = scrap.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            scrap.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, size.y);
            var move = scrap.AddComponent<ScrapMove>();
            move.speed = waveSpeed;
            move.uuid = uuid;
            var collisions = scrap.AddComponent<BoxCollider2D>();
            collisions.isTrigger = true;
            var body = scrap.AddComponent<Rigidbody2D>();
            body.gravityScale = 0;
            body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        }

        private void Start()
        {
            GenerateScrap(_scrapPositions[3], sprites[0], scrapSizes[0], "1");
        }

        public void SetScrapSpeed(float speed)
        {
            waveSpeed = speed;
        }
    }
}

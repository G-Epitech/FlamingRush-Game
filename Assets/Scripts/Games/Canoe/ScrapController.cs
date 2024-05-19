using System;
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
        private Dictionary<string, GameObject> _scraps = new();
        
        private CanoeGameController _canoeGameController;

        private Vector2[] _scrapPositions = new Vector2[4]
        {
            new Vector2(2000, 250), new Vector2(2000, 75), new Vector2(2000, -90), new Vector2(2000, -280)
        };

        private void GenerateScrap(Vector2 position, Sprite sprite, Vector2 size, string uuid, string type)
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
            scrap.GetComponent<RectTransform>().sizeDelta = size;
            var move = scrap.AddComponent<ScrapMove>();
            move.speed = waveSpeed;
            move.uuid = uuid;
            move.type = type;
            var collisions = scrap.AddComponent<BoxCollider2D>();
            collisions.isTrigger = true;
            collisions.size = size;
            var body = scrap.AddComponent<Rigidbody2D>();
            body.gravityScale = 0;
            body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
            _scraps[uuid] = scrap;
        }

        private void Start()
        {
            _canoeGameController = FindObjectOfType<CanoeGameController>();
        }

        private void Update()
        {
            if (_canoeGameController.State == null) return;
            for (int i = 0; i < _canoeGameController.State.obstacles.Count; i++)
            {
                var line = _canoeGameController.State.obstacles[i];
                foreach (var obstacle in line)
                {
                    SyncObstacle(i, obstacle.Key, obstacle.Value);
                }
            }
        }

        private void SyncObstacle(int line, string id, string type)
        {
            var sprite = type switch
            { 
                "barrel" => sprites[0],
                "log" => sprites[1],
                "alligator" => sprites[2],
                _ => null
            };
            var position = _scrapPositions[line];
            var size = type switch
            {
                "barrel" => scrapSizes[0],
                "log" => scrapSizes[1],
                "alligator" => scrapSizes[2],
                _ => Vector2.zero
            };
            if (type == "alligator")
            {
                if (_scraps.TryGetValue(id, out var alligator))
                {
                    ///TODO: Update l'alligator
                    return;
                }
            }
            if (sprite is not null && !_scraps.ContainsKey(id))
                GenerateScrap(position, sprite, size, id, type);
        }

        public void SetScrapSpeed(float speed)
        {
            waveSpeed = speed;
        }
    }
}

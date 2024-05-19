using System.Collections;
using UnityEngine;

namespace Games.Relay
{
    public class RelayPlayerController : MonoBehaviour
    {
        public int playerID;
        public bool isTurn;
        public bool isReady;
        public bool isFinished = false;
        public float speed = 50f;
        public int progress = 0;
        [SerializeField] public StadeController stadeController;
        
        private void Update()
        {
            if (isTurn && !isFinished)
            {
                progress += 75;
                stadeController.buildings.speed = speed / 8;
                stadeController.track.speed = speed / 6;
                stadeController.clouds.speed = speed / 25;
            }

            if (!isTurn && isFinished)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1000, 0, 0), 3.5f * Time.deltaTime);
            }
            
            if (progress >= 1000)
            {
                isFinished = true;
            }
        }
        
        public IEnumerator SmoothMove(RectTransform rectTransform, Vector2 endPos, float duration)
        {
            Vector2 startPos = rectTransform.anchoredPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = endPos;
        }

        public void Boost()
        {
            if (!isTurn || isFinished)
            {
                return;
            }
            
            speed += 10f;
            if (speed > 100f)
            {
                speed = 100f;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (isFinished && !isTurn)
            {
                return;
            }
            if (other.transform.position.x < transform.position.x)
            {
                return;
            }
            
            var next = other.gameObject.GetComponent<RelayPlayerController>();
            var main = FindObjectOfType<RelayGameController>();
            
            if (next != null)
            {
                next.isTurn = true;
                isTurn = false;
                isFinished = true;
                main.currentPlayerIndex += 1;
                progress = 1000000;
                StartCoroutine(SmoothMove(other.transform.GetComponent<RectTransform>(), new Vector2(-500, -260), 1f));
            }
            
        }
    }
}

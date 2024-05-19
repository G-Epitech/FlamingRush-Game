using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Games.Canoe
{
    public class ScrollController : MonoBehaviour
    {
        [SerializeField] public float[] lanesY = new float[] { 2.6f, 0.8f, -0.8f, -2.6f };
        private int _currentLaneIndex;
        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public ScrollRect scrollRect;
        [SerializeField] public GameObject player;
        [SerializeField] public float velocityThreshold = 100f;
        [SerializeField] public float moveCooldown = 0.1f;
        private bool _isMoving;

        private void Start()
        {
            _currentLaneIndex = 0;
            _isMoving = false;
        }
        
        private void Update()
        {
            var velocity = scrollRect.velocity;

            if (!_isMoving && Mathf.Abs(velocity.y) > velocityThreshold)
            {
                switch (velocity.y)
                {
                    case > 0:
                        StartCoroutine(MoveUp());
                        break;
                    case < 0:
                        StartCoroutine(MoveDown());
                        break;
                }

                scrollRect.velocity = Vector2.zero;
            }

            var targetPosition = new Vector3(player.transform.position.x, lanesY[_currentLaneIndex], player.transform.position.z);
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            scrollRect.velocity = Vector2.zero;
        }

        private IEnumerator MoveUp()
        {
            if (_currentLaneIndex <= 0) yield break;
            _isMoving = true;
            _currentLaneIndex--;
            scrollRect.velocity = Vector2.zero;
            yield return new WaitForSeconds(moveCooldown);
            _isMoving = false;
        }

        private IEnumerator MoveDown()
        {
            if (_currentLaneIndex >= lanesY.Length - 1) yield break;
            _isMoving = true;
            _currentLaneIndex++;
            scrollRect.velocity = Vector2.zero;
            yield return new WaitForSeconds(moveCooldown);
            _isMoving = false;
        }
    }


}

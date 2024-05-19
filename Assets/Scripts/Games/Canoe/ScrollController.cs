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
        [SerializeField] public GameObject[] players;
        [SerializeField] public float velocityThreshold = 100f;
        [SerializeField] public float moveCooldown = 0.1f;
        private int[] _playersIndex;
        private bool _isMoving;
        private int _index;
        private bool[] _playersDead;

        private void Start()
        {
            _currentLaneIndex = 0;
            _isMoving = false;
            _index = -1;
            _playersIndex = new int[players.Length];
            _playersDead = new bool[players.Length];

            for (int i = 0; i < _playersIndex.Length; i++)
            {
                _playersIndex[i] = 0;
            }
            for (int i = 0; i < _playersDead.Length; i++)
            {
                _playersDead[i] = false;
            }

            
            _index = 3;
            UpdatePlayerIndex(1, 0);
            UpdatePlayerIndex(2, 1);
            UpdatePlayerIndex(0, 3);
        }

        public void Update()
        {
            Compute();
            Render();
        }

        private void Compute()
        {            
            if (_index < 0) return;
            
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
            
            _playersIndex[_index] = _currentLaneIndex;
            
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
        
        public void UpdatePlayerIndex(int index, int waterline)
        {
            if (index < 0 || index >= players.Length) return;
            if (waterline < 0 || waterline >= lanesY.Length) return;
            
            _playersIndex[index] = waterline;
        }

        public void Render()
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (_playersDead[i]) continue;
                
                var targetPosition = new Vector3(players[i].transform.position.x, lanesY[_playersIndex[i]], players[i].transform.position.z);
                players[i].transform.position = Vector3.MoveTowards(players[i].transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }

        public void SetIndex(int index)
        {
            _index = index;
        }
        
        public int[] GetPlayersIndexes()
        {
            return _playersIndex;
        }
        
        public IEnumerator KillPlayer(int index)
        {
            var animator = GameObject.FindObjectOfType<Animator>();
            
            _playersDead[index] = true;
            yield return new WaitForSeconds(2.5f);
            players[index].SetActive(false);
            animator.enabled = true;
        }
    }


}

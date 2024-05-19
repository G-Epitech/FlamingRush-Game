using System;
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
        [SerializeField] public GameObject[] buttons;
        [SerializeField] public float velocityThreshold = 100f;
        [SerializeField] public float moveCooldown = 0.1f;
        private int[] _playersIndex;
        private bool _isMoving;
        private int _index;
        private bool[] _playersDead;
        private CanoeGameController _canoeGameController;
        private Animator _animator;

        private void Awake()
        {
            _canoeGameController = FindObjectOfType<CanoeGameController>();
            _animator = FindObjectOfType<Animator>();
        }

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
        }

        public void Update()
        {
            Sync();
            Render();
        }

        private void Sync()
        {
            if (_canoeGameController.State == null) return;
            _index = _canoeGameController.State.players[_canoeGameController.gameManager.id].x;
            foreach (var (id, position) in _canoeGameController.State.players)
            {
                if (_playersDead[position.x])
                    continue;

                if (id == _canoeGameController.gameManager.id)
                {
                    var obj = players[position.x];
                    var me = obj.transform.GetChild(0);
                    me.gameObject.SetActive(true);
                    if (position.y < 0) {
                        foreach (var button in buttons)
                            button.SetActive(false);
                    } else {
                        _currentLaneIndex = position.y;
                    }
                }
                
                if (position.y < 0)
                    StartCoroutine(KillPlayer(position.x));
                else
                    UpdatePlayerIndex(position.x, position.y);
            }
        }

        private IEnumerator MoveUp()
        {
            if (_currentLaneIndex <= 0) yield break;
            _canoeGameController.EmitPosition(_index, _currentLaneIndex - 1);
        }

        private IEnumerator MoveDown()
        {
            if (_currentLaneIndex >= lanesY.Length - 1) yield break;
            _canoeGameController.EmitPosition(_index, _currentLaneIndex + 1);
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

                var targetPosition = new Vector3(players[i].transform.position.x, lanesY[_playersIndex[i]],
                    players[i].transform.position.z);
                players[i].transform.position = Vector3.MoveTowards(players[i].transform.position, targetPosition,
                    moveSpeed * Time.deltaTime);
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
            _playersDead[index] = true;
            players[index].GetComponent<CanoeController>().Kill();
            yield return new WaitForSeconds(2.5f);
            players[index].SetActive(false);
            _animator.enabled = true;
        }

        public void PressUp()
        {
            StartCoroutine(MoveUp());
        }

        public void PressDown()
        {
            StartCoroutine(MoveDown());
        }
    }
}
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Canoe
{
    public class CanoeController : MonoBehaviour
    {
        [SerializeField] public Animator animator;
        [SerializeField] public Sprite deadSprite;
        [SerializeField] public int ID;
        private CanoeGameController _canoeGameController;
    
        /// <summary>
        /// This Function accelerates ALL canoes in the scene.
        /// The default speed is 1.
        /// </summary>
        public void Accelerate(float speed)
        {
            animator.speed = speed;
        }

        private void Start()
        {
            _canoeGameController = FindObjectOfType<CanoeGameController>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var scrap = other.GetComponent<ScrapMove>();
            if (scrap == null) return;

            var players = _canoeGameController.State.players;
            var me = players.FirstOrDefault(p => p.Key == _canoeGameController.gameManager.id);
            if (me.Value.x == ID)
                _canoeGameController.EmitCollision(scrap.uuid, scrap.type);
        }

        public void Kill()
        {
            var me = _canoeGameController.State.players[_canoeGameController.gameManager.id];

            if (me.x == ID)
                Announcement.announce(Announcement.AnnouncementType.LOSE);
            animator.enabled = false;
            var img = gameObject.GetComponent<Image>();
            img.sprite = deadSprite;
        }
    }
}

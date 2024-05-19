using System;
using System.Collections;
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
            
            _canoeGameController.EmitCollision(scrap.uuid, scrap.type);
            
            Destroy(other.gameObject);
            animator.enabled = false;
            
            var img = gameObject.GetComponent<Image>();
            img.sprite = deadSprite;
        }
    }
}

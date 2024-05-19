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
    
        /// <summary>
        /// This Function accelerates ALL canoes in the scene.
        /// The default speed is 1.
        /// </summary>
        public void Accelerate(float speed)
        {
            animator.speed = speed;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var scrollController = GameObject.FindObjectOfType<ScrollController>();
            var oldSpeed = animator.speed;
            
            Destroy(other.gameObject);
            Debug.Log("I'm DEAD !");
            animator.enabled = false;
            
            var img = gameObject.GetComponent<Image>();
            img.sprite = deadSprite;
            StartCoroutine(scrollController.KillPlayer(ID));
            Debug.Log("I'm really 5 DEAD !");
            // StartCoroutine(Die());
            Debug.Log("I'm really 4 DEAD !");
        }
        

    }
}

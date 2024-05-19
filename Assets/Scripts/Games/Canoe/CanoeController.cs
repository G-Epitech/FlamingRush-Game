using System;
using UnityEngine;

namespace Games.Canoe
{
    public class CanoeController : MonoBehaviour
    {
        [SerializeField] public Animator animator;
    
        /// <summary>
        /// This Function accelerates ALL canoes in the scene.
        /// The default speed is 1.
        /// </summary>
        public void Accelerate(float speed)
        {
            animator.speed = speed;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Collision detected");
        }
    }
}

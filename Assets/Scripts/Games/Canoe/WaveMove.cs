using System;
using UnityEngine;

namespace Games.Canoe
{
    public class WaveMove : MonoBehaviour
    {
        public float speed = 2f; // Vitesse de déplacement

        private void Update()
        {
            if (transform.position.x < -20.0f)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(Vector3.left * (speed * Time.deltaTime));    
            }
        }
    }
}
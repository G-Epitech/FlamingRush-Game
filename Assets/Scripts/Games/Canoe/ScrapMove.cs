using System;
using UnityEngine;

namespace Games.Canoe
{
    public class ScrapMove : MonoBehaviour
    {
        public float speed = 2f;
        public string uuid;

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

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision detected");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("DESTROY");
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Scrap collected");
                Destroy(gameObject);
            }
        }
    }
}
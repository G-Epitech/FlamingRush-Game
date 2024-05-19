using UnityEngine;
using UnityEngine.UI;

namespace Games.Relay
{
    public class CloudBackground : MonoBehaviour
    {
        [SerializeField] public int numberOfSegments;
        [SerializeField] public Vector2 segmentSize;
        [SerializeField] public float speed;
        
        private void Start()
        {
            for (int i = 0; i < numberOfSegments; i++)
            {
                var segment = new GameObject("Segment");
                segment.transform.SetParent(transform);
                segment.transform.localScale = Vector3.one;
                segment.transform.localPosition = new Vector3(segmentSize.x * i, 0, 0);
                var rect = segment.AddComponent<RectTransform>();
                rect.sizeDelta = segmentSize;
                var image = segment.AddComponent<Image>();
                image.sprite = transform.GetComponent<Image>().sprite;
                image.rectTransform.sizeDelta = segmentSize;
            }
            transform.GetComponent<Image>().enabled = false;
        }
        
        private void Update()
        {
            foreach (Transform segment in transform)
            {
                segment.Translate(Vector3.left * (Time.deltaTime * speed));
                if (segment.position.x < -(segmentSize.x))
                {
                    segment.Translate(Vector3.right * (segmentSize.x * numberOfSegments));
                }
            }
        }
    }
}

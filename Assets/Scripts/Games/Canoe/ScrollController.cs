using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Games.Canoe
{
    public class ScrollController : MonoBehaviour
    {
        public ScrollRect scrollRect; // Référence au ScrollRect
        public GameObject character; // Référence au RectTransform du personnage

        public float position1 = -2f;
        public float position2 = -0.75f;
        public float position3 = 0.75f;
        public float position4 = 2f;
        public int currentPosition = 2;
        
        public void Update()
        {
            var scrollPosition = scrollRect.velocity.y;
            var characterPosition = character.transform.localPosition;
            
            if (characterPosition.y < -8f)
            {
                character.transform.localPosition = new Vector3(characterPosition.x, -8f, characterPosition.z);
            }
            else if (characterPosition.y > 8f)
            {
                character.transform.localPosition = new Vector3(characterPosition.x, 8f, characterPosition.z);
            }
            else
            {
                character.transform.localPosition = new Vector3(characterPosition.x, characterPosition.y + scrollPosition * 0.0001f, characterPosition.z);
            }
            
        }
    }
    

}

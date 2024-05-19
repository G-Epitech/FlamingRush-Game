using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lobby
{
    public class StartController : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private Sprite waitingSprite;
        [SerializeField] private Sprite readyYesSprite;
        [SerializeField] private Sprite readyNoSprite;
        [SerializeField] private Sprite startSprite;
        [SerializeField] private Button button;

        public void StartGame()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ChangeToReady()
        {
            button.image.sprite = readyYesSprite;
        }

        public void ChangeToNotReady()
        {
            button.image.sprite = readyNoSprite;
        }

        public void ChangeToStart()
        {
            button.image.sprite = startSprite;
        }

        public void ChangeToWaiting()
        {
            button.image.sprite = waitingSprite;
        }
    }
}
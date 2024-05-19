using UnityEngine;

namespace Games.Relay
{
    public class StadeController : MonoBehaviour
    {
        [SerializeField] public Background track;
        [SerializeField] public Background buildings;
        [SerializeField] public CloudBackground clouds;
        
        private void Start()
        {
            track.speed = 12f;
            buildings.speed = 8f;
            clouds.speed = 2f;
        }
    }
}

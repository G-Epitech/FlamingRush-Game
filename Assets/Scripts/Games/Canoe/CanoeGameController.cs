using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Games.Canoe
{
    public class Position
    {
        public int x;
        public int y;
    }

    public class State
    {
        public string type;
        public Dictionary<string, Position> players;
        public List<Dictionary<string, string>> obstacles;
    }

    public class PositioDTO
    {
        public Position position;
    }
    
    public class CollisionDTO
    {
        public string id;
        public string obstacle;
    }

    public class CanoeGameController : MonoBehaviour
    {
        public GameManager gameManager { get; private set; }

        public State State;
        private void OnDestroy()
        {
            Announcement.announce(Announcement.AnnouncementType.FINISH);
            gameManager?.client.Off("games/update");
        }

        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager?.client.OnUnityThread("games/state", response => UpdateState(response.GetValue<State>()));
            Announcement.announce(Announcement.AnnouncementType.START);
        }
        
        private void UpdateState(State state)
        {
            State = state;
        }
        
        public void EmitPosition(int x, int y)
        {
            var position = new PositioDTO
            {
                position = new Position  {
                    x = x,
                    y = y
                }
            };
            gameManager?.client.Emit("games/canoe/move", position);
            Debug.Log("Emitting position");
        }
        
        public void EmitCollision(string uuid, string type)
        {
            var collision = new CollisionDTO
            {
                id = uuid,
                obstacle = type
            };
            gameManager?.client.Emit("games/canoe/collide", collision);
        }
    }
}

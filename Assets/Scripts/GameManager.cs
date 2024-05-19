using System;
using Lobby;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private SocketIOUnity _client;
    private string _id;
    public PlayerData data;
    public GameData gameData;

    private async void Start()
    {
        this.data = CacheSystem.loadPlayerData();
        if (GameManager.Instance == null)
            return;
        var uri = new Uri("http://localhost:3000");
        _client = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        _client.JsonSerializer = new NewtonsoftJsonSerializer();
        await _client.ConnectAsync();

        this.RegisterBaseEvents();

        _client.Emit("user/new");
        this.data = CacheSystem.loadPlayerData();
        data.id = _id;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private struct NewClient
    {
        public string id;
    }

    private struct RoomUser
    {
        public string id;
        public string name;
        public int profilePicture;
        public bool owner;
        public bool ready;
    }

    private struct Room
    {
        public string id;
        public RoomUser[] users;
    }

    private struct StartGame
    {
        public string type;
    }

    private void RegisterBaseEvents()
    {
        _client.On("user/created", (response) => { this._id = response.GetValue<NewClient>(0).id; });
        _client.OnUnityThread("room/updated", (response) =>
        {
            if (SceneManager.GetActiveScene().name != "Lobby")
            {
                SceneManager.LoadScene("Lobby");
                return;
            }

            var playerControllerObject = GameObject.FindWithTag("PlayersController");
            PlayerController playerController = playerControllerObject.GetComponent<PlayerController>();

            RoomUser me = new RoomUser();
            bool allReady = true;
            var room = response.GetValue<Room>();
            for (int i = 0; i < room.users.Length; i++)
            {
                var user = room.users[i];
                bool isMe = user.id == this._id;
                playerController.SetPlayer(i + 1, user.name, user.profilePicture, isMe, user.ready);

                if (isMe)
                {
                    me = user;
                    this.data.position = i;
                }

                if (!user.ready)
                    allReady = false;
            }

            if (room.users.Length < 4)
                allReady = false;

            playerController.SetRoomCode(room.id);
            
            var startControllerObject = GameObject.FindWithTag("StartController");
            StartController startController = startControllerObject.GetComponent<StartController>();

            if (me.owner)
            {
                if (!allReady)
                    startController.ChangeToWaiting();
                else
                    startController.ChangeToStart();
            }
            else
            {
                if (!me.ready)
                    startController.ChangeToNotReady();
                else if (allReady)
                    startController.ChangeToWaiting();
                else
                    startController.ChangeToReady();
            }
        });
        
        _client.OnUnityThread("room/start-round", (response) =>
        {
            var data = response.GetValue<StartGame>();
            if (data.type == "canoe")
            {
                var fade = GameObject.FindObjectOfType<Fade>(true);
                
                fade.FadeIn("Canoe");
            }
        });
    }

    private struct CreateGame
    {
        public string name;
        public int profilePicture;
    }

    public void createNewGame()
    {
        var data = new CreateGame()
        {
            name = this.data.name,
            profilePicture = this.data.profilePictureIdx,
        };

        _client.Emit("room/create", data);
    }

    private struct JoinGame
    {
        public string code;
        public string name;
        public int profilePicture;
    }

    public void joinGame(GameObject inputObject)
    {
        TextMeshProUGUI text = inputObject.GetComponent<TextMeshProUGUI>();

        var data = new JoinGame()
        {
            code = text.text.Substring(0, 6),
            name = this.data.name,
            profilePicture = this.data.profilePictureIdx,
        };
        _client.Emit("room/join", data);
    }

    public void askRoomStatus()
    {
        _client.Emit("room/status");
    }

    public void setReady()
    {
        _client.Emit("room/user-ready");
    }
    
    public void startRound()
    {
        _client.Emit("room/start-round");
    }

    public SocketIOUnity GetSocketClient()
    {
        return _client;
    }
}
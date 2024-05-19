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
    [SerializeField] private GameObject _tutorialModal;
    [SerializeField] private GameObject _menuBackdrop;
    public static GameManager Instance;

    public bool cacheExists;
    public SocketIOUnity client { get; private set; }
    public string id { get; private set; }
    public int order { get; private set; }
    public PlayerData data;
    public GameData gameData;
    
    private async void Start()
    {
        this.data = CacheSystem.loadPlayerData();
        this.gameData = new GameData();

        cacheExists = true;
        if (CacheSystem.cacheExists() == false)
        {
            cacheExists = false;
            _tutorialModal.SetActive(true);
            _menuBackdrop.SetActive(true);
        }
        if (Instance == null)
            return;
        
        var uri = new Uri("http://10.29.126.76:3000");
        client = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        client.JsonSerializer = new NewtonsoftJsonSerializer();

        await client.ConnectAsync();

        this.RegisterBaseEvents();

        client.Emit("user/new");
        data.id = id;
        data.order = order;
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
        public int order;
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
    
    private struct EndGame
    {
        public string type;
        public int round;
        public int lives;
    }

    private void RegisterBaseEvents()
    {
        client.On("user/created", (response) => { this.id = response.GetValue<NewClient>(0).id; });
        client.OnUnityThread("room/updated", (response) =>
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
                bool isMe = user.id == this.id;
                playerController.SetPlayer(i + 1, user.name, user.profilePicture, isMe, user.ready);

                if (isMe)
                {
                    me = user;
                    this.data.order = i;
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
        
        client.OnUnityThread("room/start-round", (response) =>
        {
            var data = response.GetValue<StartGame>();
            if (data.type == "canoe")
            {
                var fade = GameObject.FindObjectOfType<Fade>(true);
                
                fade.FadeIn("Canoe");
            }
        });
        
        client.OnUnityThread("room/end-round", (response) =>
        {
            var data = response.GetValue<EndGame>();
            this.gameData.lifes = (uint) data.lives;
            this.gameData.streak = (uint) data.round;
            var fade = GameObject.FindObjectOfType<Fade>(true);
            
            fade.FadeIn("FlameScore");
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

        client.Emit("room/create", data);
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
        client.Emit("room/join", data);
    }

    public void askRoomStatus()
    {
        client.Emit("room/status");
    }

    public void setReady()
    {
        client.Emit("room/user-ready");
    }

    public void startRound()
    {
        client.Emit("room/start-round");
    }
}
using System;
using Lobby;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private SocketIOUnity _client;
    private string _id;
    public PlayerData data;
    public bool cacheExists;

    private async void Start()
    {
        cacheExists = true;
        this.data = CacheSystem.loadPlayerData();
        if (data.id == "no_id")
        {
            this.cacheExists = false;
        }
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(SceneManager.loadedSceneCount);
        if (!cacheExists)
            return;
        GameObject gameObject = GameObject.FindGameObjectWithTag("tutorialModal");
        if (gameObject != null)
        {
            Debug.Log("start tutorial");
            gameObject.SetActive(true);
        }
        gameObject = GameObject.FindGameObjectWithTag("menuBackdrop");
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
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

            var room = response.GetValue<Room>();
            for (int i = 0; i < room.users.Length; i++)
            {
                var user = room.users[i];
                bool isMe = user.id == this._id;
                playerController.SetPlayer(i + 1, user.name, isMe, user.ready);
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
            name = "Dragos",
            profilePicture = 2,
        };

        _client.Emit("room/create", data);
    }

    public void askRoomStatus()
    {
        _client.Emit("room/status");
    }
}
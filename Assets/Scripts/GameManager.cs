using System;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private SocketIOUnity _client;
    private string _id;

    private async void Start()
    {
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

    private void RegisterBaseEvents()
    {
        _client.On("user/created", (response) => { this._id = response.GetValue<NewClient>(0).id; });
        _client.OnUnityThread("room/updated", (response) =>
        {
            SceneManager.LoadScene("Lobby");
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
}

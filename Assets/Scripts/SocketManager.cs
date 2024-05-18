using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    public static SocketManager Instance;

    private SocketIOUnity _client;
    private string _id;

    struct NewClient
    {
        public string id;
    }

    private async void Start()
    {
        if (SocketManager.Instance == null)
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

    private void RegisterBaseEvents()
    {
        _client.On("user/created", (response) =>
        {
            this._id = response.GetValue<NewClient>(0).id;
        });
    }
}

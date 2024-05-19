using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name {  get; set; }
    public string id { get; set; }
    public string profilePicture {  get; set; }

    public PlayerData()
    {
        name = string.Empty;
        id = string.Empty;
        profilePicture = string.Empty;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name {  get; set; }
    public string id { get; set; }
    public int profilePictureIdx {  get; set; }

    public PlayerData()
    {
        name = string.Empty;
        id = string.Empty;
        profilePictureIdx = 0;
    }
}

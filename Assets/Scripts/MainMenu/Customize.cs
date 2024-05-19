using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private Image _profilePicture;

    private void Awake()
    {
        Player player = Player.Instance;
        if (player == null)
        {
            Debug.Log("Player instance is null");
            return;
        }
        _inputField.text = player.data.name;
    }

    public void ConfirmChanges()
    {
        Player player = Player.Instance;
        if (player == null)
        {
            Debug.Log("Player instance is null");
            return;
        }
        player.data.name = _inputField.text;
        player.data.profilePicture = _profilePicture.name;
        CacheSystem.savePlayerData(player.data);
        Debug.Log(player.data.id);
    }
}

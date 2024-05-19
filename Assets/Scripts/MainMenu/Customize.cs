using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    private void Awake()
    {
        GameManager gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.Log("Player instance is null");
            return;
        }
        _inputField.text = gm.data.name;
    }

    public void ConfirmChanges()
    {
        GameManager gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.Log("Player instance is null");
            return;
        }
        gm.data.name = _inputField.text;
        CacheSystem.savePlayerData(gm.data);
    }
}

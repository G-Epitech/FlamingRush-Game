using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}

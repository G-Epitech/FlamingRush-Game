using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class LeaveController : MonoBehaviour
    {
        public void Leave()
        {
            Debug.Log("Enter");
            SceneManager.LoadScene("MainMenu");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ReturnMenuController : MonoBehaviour
{
    [SerializeField] private Fade fade;
    
    public void ReturnMenu()
    {
        fade.FadeIn("MainMenu");
    }
}

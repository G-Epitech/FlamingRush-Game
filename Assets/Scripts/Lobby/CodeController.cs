using System;
using TMPro;
using UnityEngine;

public class CodeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI codeText;
    
    public void SetCode(string code)
    {
        codeText.text = code;
    }

    public void Start()
    {
        SetCode("ABC124");
    }
}

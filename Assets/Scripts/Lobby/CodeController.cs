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

    public void CopyCode()
    {
        TextEditor te = new TextEditor();
        te.text = codeText.text;
        te.SelectAll();
        te.Copy();
    }
}

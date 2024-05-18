using TMPro;
using UnityEngine;

public class ButtonText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;

    public void ChangeText(string newText)
    {
        buttonText.text = newText;
    }
    
    // Change on start
    private void Start()
    {
        ChangeText("Hello World");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonClick : MonoBehaviour
{
    public GameObject backdrop;

    public void openModal(GameObject gameObject)
    {
        backdrop.SetActive(true);
        gameObject.SetActive(true);
    }

    public void closeModal(GameObject gameObject)
    {
        backdrop.SetActive(false);
        gameObject.SetActive(false);
    }
}

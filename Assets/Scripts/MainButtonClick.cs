using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonClick : MonoBehaviour
{
    public GameObject backdrop;

    public void openModal(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void closeModal(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void activateBackdrop()
    {
        backdrop.SetActive(true);
    }

    public void deactivateBackdrop()
    {
        backdrop.SetActive(false);
    }
}

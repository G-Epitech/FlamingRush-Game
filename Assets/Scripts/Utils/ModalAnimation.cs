using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalAnimation : MonoBehaviour
{
    public Transform box;
    public GameObject backdrop;

    private void OnEnable()
    {
        backdrop.SetActive(true);
        CanvasGroup background = backdrop.GetComponent<CanvasGroup>();
        background.LeanAlpha(0.5f, 0.5f);
        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialog()
    {
        CanvasGroup background = backdrop.GetComponent<CanvasGroup>();
        background.LeanAlpha(0f, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(onComplete);
    }

    void onComplete()
    {
        gameObject.SetActive(false);
        backdrop.SetActive(false);
    }
}

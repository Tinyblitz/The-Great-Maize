using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResumeButton : MonoBehaviour
{
    public Canvas pauseCanvas;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = pauseCanvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("doesn’t find the component Canvas Group");
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;        // Time.timeScale is a simple way to pause your game in a whole background
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }
}

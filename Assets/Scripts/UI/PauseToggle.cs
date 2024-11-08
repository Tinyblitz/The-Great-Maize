using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Canvas Group allows you to control all child UI components and their descendants in a coordinated fashion
[RequireComponent(typeof(CanvasGroup))]
public class PauseToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private GameObject pauseMenu;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("doesn’t find the component Canvas Group");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu = GameObject.Find("Pause Menu");
        //pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // This will allow multiple game controllers to map to common input events
        // (e.g. simultaneous keyboard, and handheld game controller support)
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvasGroup.interactable)   // when the in-game menu is visible
            {
                Time.timeScale = 1f;        // Time.timeScale is a simple way to pause your game in a whole background
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;

            }
            else                            //  when the menu is off
            {
                Time.timeScale = 0f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;


            }
        }

        

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Canvas Group allows you to control all child UI components and their descendants in a coordinated fashion
[RequireComponent(typeof(CanvasGroup))]
public class gameOverScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private GameObject pauseMenu;

    public static gameOverScript instance;

    public float countdown = 0;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("doesn’t find the component Canvas Group");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        countdown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // This will allow multiple game controllers to map to common input events
        // (e.g. simultaneous keyboard, and handheld game controller support)
        if (PlayerControlScript.instance.hp <= 0)
        {

            countdown += Time.deltaTime;

            if (countdown >= 2)   // when the in-game menu is visible
            {
                Time.timeScale = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;

            }
        }



    }
}


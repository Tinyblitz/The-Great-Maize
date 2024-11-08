using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Animation for Boss Cutscene.
public class BossCutScene : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public GameObject Mushroom;
    public GameObject Boss;

    private AudioSource source;
    public AudioClip heavyStep;
    public AudioClip roar;

    private float timeTracker;

    private bool isMushDead;
    private bool bossEnters;
    private bool camChange;
    private bool soundChange;
    private bool sceneEnd;


    private Vector3 targetDirection;

    void Start()
    {
        timeTracker = Time.time;
        isMushDead = false;
        bossEnters = false;
        camChange = false;
        soundChange = false;
        sceneEnd = false;


        source = GetComponent<AudioSource>();

        player.transform.eulerAngles = new Vector3(0, 90, 0);
        player.transform.position = new Vector3(-7, 0.5f, -2);

        Mushroom.transform.eulerAngles = new Vector3(0, -90, 0);
        Mushroom.transform.position = new Vector3(0, 0.15f, -2);

        targetDirection = new Vector3(-1, 0.15f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneEnd)
        {
            // Time till change to boss screen
            if (Time.time - timeTracker >= 4.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            // Blackout Screen
            cam.transform.position = new Vector3(-5.02f, 5.38f, -30.47f);
            cam.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (bossEnters)
        {
            // Camera Zooms every 2 seconds
            if (Time.time - timeTracker >= 13.0f)
            {
                
                source.PlayOneShot(roar, 5.0f);

                sceneEnd = true;
                timeTracker = Time.time;
            }
            else if (Time.time - timeTracker >= 10.0f)
            {
                if (cam.transform.position != new Vector3(-1.94f, 6.29f, 7.01f))
                {
                    source.PlayOneShot(heavyStep, 10.0f);
                }
                cam.transform.position = new Vector3(-1.94f, 6.29f, 7.01f);
                cam.transform.eulerAngles = new Vector3(7.426f, -0.789f, -0.515f);
            }
            else if (Time.time - timeTracker >= 8.0f)
            {
                if (cam.transform.position != new Vector3(-1.14f, 4.36f, 3.45f))
                {
                    source.PlayOneShot(heavyStep, 5.0f);
                }
                cam.transform.position = new Vector3(-1.14f, 4.36f, 3.45f);
                cam.transform.eulerAngles = new Vector3(0.769f, -7.423f, -1.376f);
            }
            else if (Time.time - timeTracker >= 6.0f)
            {
                if (cam.transform.position != new Vector3(0.21f, 2.52f, -2.09f))
                {
                    source.PlayOneShot(heavyStep, 3.0f);
                }
                cam.transform.position = new Vector3(0.21f, 2.52f, -2.09f);
                cam.transform.eulerAngles = new Vector3(-6.568f, -14.033f, -1.467f);
            }
            // Player rotates towards sound
            else if (Time.time - timeTracker >= 4.0f)
            {
                // The step size is equal to speed times frame time.
                float singleStep = 0.01f * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Calculate a rotation a step closer to the target and applies rotation to this object

                player.transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else if (Time.time - timeTracker >= 3.0f)
            {
                if (!soundChange)
                {
                    source.PlayOneShot(heavyStep, 2.0f);
                    soundChange = true;
                }
            }
        }
        else
        {
            if (isMushDead)
            {
                // Player Picks Up resource and camera detaches
                if (Time.time - timeTracker >= 3.0f)
                {
                    if (player.transform.position.x <= -1)
                    {
                        if (player.transform.position.x >= -3)
                        {
                            cam.transform.position = new Vector3(1.43f, 0.5f, -4.53f);
                            cam.transform.eulerAngles = new Vector3(-6.729f, -26.653f, 0);
                            camChange = true;
                        }
                        player.transform.position = new Vector3(player.transform.position.x + 0.04f, 0.15f, -2);
                    }
                    else
                    {
                        timeTracker = Time.time;
                        bossEnters = true;
                        source.PlayOneShot(heavyStep, 1.0f);
                    }
                    
                }
            }
            // Starting scene, Mushroom gets killed
            else
            {
                // Mushroom gets hit back and dies
                if (!Mushroom.GetComponent<Animator>().GetBool("isDamaged"))
                {
                    // Moves towards player
                    Mushroom.transform.position = new Vector3(Mushroom.transform.position.x - 0.04f, 0.15f, -2);
                }
                else
                {
                    if (Mushroom.transform.position.x >= -1)
                    {
                        Mushroom.GetComponent<Animator>().SetBool("Dead", true);
                        isMushDead = true;
                        timeTracker = Time.time;
                    }
                    else
                    {
                        // Pushed away from player
                        Mushroom.transform.position = new Vector3(Mushroom.transform.position.x + 0.05f, 0.15f, -2);
                    }
                }

                // Mushroom gets close enough, player attacks
                if (Mushroom.transform.position.x <= -5.5)
                {

                    player.GetComponent<Animator>().SetTrigger("attackOne");
                    Mushroom.GetComponent<Animator>().SetBool("isDamaged", true);


                }
            }
            if (!camChange)
            {
                cam.transform.position = new Vector3(player.transform.position.x + 0.2f, player.transform.position.y + 1.0f, player.transform.position.z + 0.2f);
                cam.transform.forward = player.transform.forward;
            }
        }
    }
}


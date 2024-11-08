using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public GameObject Mushroom;
    public GameObject cabbage;
    public GameObject doors;
    public GameObject crate;


    private float timeTracker;

    private float t = 0.0f;
    private float m = 0.0f;
    private float c = 0.0f;
    private bool rotateChange = false;
    private bool isDoorOpen = false;
    private bool isInside = false;
    private bool cratesFallen = false;
    private bool mushAttack = false;
    private bool mushDidCry = false;
    private bool sceneOver = false;

    private AudioSource source;
    public AudioClip mushCry;
    public AudioClip slideOpen;
    public AudioClip slideClose;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        timeTracker = Time.time;

        cam.transform.position = new Vector3(-1.92f, 1.72f, 19.99f);
        cam.transform.eulerAngles = new Vector3(0, 180.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneOver)
        {
            // Time till change to title screen
            if (Time.time - timeTracker >= 1.5f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            // Blackout Screen
            cam.transform.position = new Vector3(-4.4f, 0.73f, -31.86f);
            cam.transform.eulerAngles = new Vector3(0, 0.0f, 0);
        }
        else if (mushAttack)
        {
            if (Time.time - timeTracker >= 1.3f)
            {
                float transY = Mathf.Lerp(3.15f, 1.45f, m);
                float transZ = Mathf.Lerp(4.48f, 1.53f, m);

                if (m < 1.0f)
                {
                    m += Time.deltaTime / 0.5f;
                }
                else
                {
                    sceneOver = true;
                    timeTracker = Time.time;
                }

                Mushroom.transform.position = new Vector3(-1.91f, transY, transZ);
            }
            else if (Time.time - timeTracker >= 1.0f) {
                float rotateY = Mathf.Lerp(180.0f, 0.0f, t);
                float rotateX = Mathf.Lerp(0.0f, -10.0f, t);

                if (t < 1.0f)
                {
                    t += Time.deltaTime / 0.25f;
                }

                cam.transform.eulerAngles = new Vector3(rotateX, rotateY, 0.0f);
                Mushroom.transform.position = new Vector3(-1.91f, 3.15f, 4.48f);
                Mushroom.transform.eulerAngles = new Vector3(30.0f, 180.0f, 0);
            }
        }
        else if (cratesFallen)
        {
            if (cam.transform.position.z <= 0.0f)
            {
                t = 0.0f;
                timeTracker = Time.time;
                if (!mushDidCry) 
                {
                    source.PlayOneShot(mushCry, 1.0f);
                    mushDidCry = true;
                    mushAttack = true;
                }
            }
            else if (Time.time - timeTracker >= 2.0f) 
            {
                cam.transform.position = new Vector3(-1.92f, 1.72f, cam.transform.position.z - 0.04f);
                float rotate = Mathf.Lerp(62.0f, 180.0f, t);

                if (t < 1.0f)
                {
                    t += Time.deltaTime / 0.5f;
                }

                cam.transform.eulerAngles = new Vector3(0.0f, rotate, 0.0f);
            }
        }
        else if (isInside)
        {
            if (cam.transform.position.z <= 3.0f)
            {
                float rotate = Mathf.Lerp(180.0f, 62.0f, t);

                if (t < 1.0f)
                {
                    t += Time.deltaTime / 0.25f;
                }
                else
                {
                    cratesFallen = true;
                    t = 0.0f;
                    timeTracker = Time.time;
                }

                cam.transform.eulerAngles = new Vector3(0.0f, rotate, 0.0f);
            }
            else if (cam.transform.position.z <= 3.5f)
            {
                timeTracker = Time.time;
                crate.transform.eulerAngles = new Vector3(13.0f, -180f, 0);
                cam.transform.position = new Vector3(-1.92f, 1.72f, cam.transform.position.z - 0.04f);
            }
            else if (cam.transform.position.z <= 5.0f)
            {
                
                t = 0.0f;
                
                cam.transform.position = new Vector3(-1.92f, 1.72f, cam.transform.position.z - 0.04f);
            }
            else if (Time.time - timeTracker >= 4.0f)
            {
                if (rotateChange)
                {
                    t = 0.0f;
                    rotateChange = false;
                }

                cam.transform.position = new Vector3(-1.92f, 1.72f, cam.transform.position.z - 0.04f);

                float rotate = Mathf.Lerp(250.0f, 180.0f, t);

                if (t < 1.0f)
                {
                    t += Time.deltaTime / 2.0f;
                }

                cam.transform.eulerAngles = new Vector3(0.0f, rotate, 0.0f);
            }
            else if (Time.time - timeTracker >= 1.0f)
            {
                float rotate = Mathf.Lerp(100.0f, 250.0f, t);

                if (t < 1.0f)
                {
                    t += Time.deltaTime / 2.0f;
                }
                else {
                    rotateChange = true;
                }

                cam.transform.eulerAngles = new Vector3(0.0f, rotate, 0.0f);

                if (Time.time - timeTracker >= 2.0f)
                {
                    float transZ = Mathf.Lerp(10.29f, 9.0f, c);

                    if (c < 1.0f)
                    {
                        c += Time.deltaTime / 2.0f;
                    }

                    cabbage.transform.position = new Vector3(-10.0f, 0.21f, transZ);
                }

            }
        }
        else
        {
            if (cam.transform.position.z <= 10.0f)
            {
                if (isDoorOpen)
                {
                    doors.GetComponent<Animator>().SetTrigger("DoorClose");
                    source.PlayOneShot(slideClose, 1.0f);

                    isDoorOpen = false;
                }

                float rotate = Mathf.Lerp(180.0f, 100.0f, t);

                if (t < 1.0f)
                {
                    t += Time.deltaTime / 2.0f;
                }
                else
                {
                    isInside = true;
                    t = 0.0f;
                    timeTracker = Time.time;
                }

                cam.transform.eulerAngles = new Vector3(0.0f, rotate, 0.0f);
            }
            else if (Time.time - timeTracker >= 5.0f)
            {
                t = 0.0f;
                cam.transform.position = new Vector3(-1.92f, 1.72f, cam.transform.position.z - 0.04f);

                if (cam.transform.position.z <= 15.0f && !isDoorOpen)
                {
                    isDoorOpen = true;
                    doors.GetComponent<Animator>().SetTrigger("DoorOpen");
                    source.PlayOneShot(slideOpen, 1.0f);
                }

            }
            else if (Time.time - timeTracker >= 3.0f)
            {
                if (rotateChange)
                {
                    t = 0.0f;
                    rotateChange = false;
                }

                float rotate = Mathf.Lerp(-20.853f, 0.0f, t);

                if (t <= 1.0f)
                {
                    t += Time.deltaTime / 2.0f;
                }
                cam.transform.eulerAngles = new Vector3(rotate, 180.0f, 0);
            }
            else
            {
                float rotate = Mathf.Lerp(0.0f, 20.853f, t);

                if (t < 1.0f)
                {
                    t += Time.deltaTime / 2.0f;
                }
                else
                {
                    rotateChange = true;
                }


                cam.transform.eulerAngles = new Vector3(-rotate, 180.0f, 0);
            }
        }
    }
}

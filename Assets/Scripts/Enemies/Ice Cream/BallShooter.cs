using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BallShooter : MonoBehaviour
{
    public float firingRate;            // In Seconds
    public float bulletSpeed = 10;
    public Rigidbody creamPrefab_red;
    public Rigidbody creamPrefab_blue;
    public Rigidbody creamPrefab_green;
    public Rigidbody creamPrefab_orange;

    public AudioClip shot_sound;
    public AudioClip death_sound;


    private float timeTracker;
    private Animator anim;
    private Transform ballPosition;
    private Rigidbody currBall;
    private bool hasCream = false;

    private float creamTracker;

    private AudioSource source;

    void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        ballPosition = this.transform.Find("IceBallPos");

        creamTracker = 4.0f;
        while (creamTracker == 4.0f)
        {
            creamTracker = Mathf.Floor(Random.Range(0f, 4.0f));
        }
        if (creamPrefab_red == null)
        {
            Debug.LogError("No Red Ice Cream prefab");
        }

        if (creamPrefab_blue == null)
        {
            Debug.LogError("No Blue Ice Cream prefab");
        }

        if (creamPrefab_green == null)
        {
            Debug.LogError("No Green Ice Cream prefab");
        }

        if (creamPrefab_orange == null)
        {
            Debug.LogError("No Orange Ice Cream prefab");
        }

        source = GetComponent<AudioSource>();

        float buffer = Random.Range(0f, 3.0f);

        timeTracker = Time.time + buffer;
    }

    void Update()
    {
        if (anim.GetBool("Dead"))
        {
            return;
        }
        // Reload Shot
        if (!anim.GetBool("isFiring"))
        {
            if (!hasCream)
            {
                hasCream = true;
                Rigidbody cream = null;

                if (creamTracker == 0)
                {
                    cream = creamPrefab_red;
                }
                else if (creamTracker == 1)
                {
                    cream = creamPrefab_blue;
                }
                else if (creamTracker == 2)
                {
                    cream = creamPrefab_green;
                }
                else if (creamTracker == 3)
                {
                    cream = creamPrefab_orange;
                }
                creamTracker++;

                if (creamTracker > 3)
                {
                    creamTracker = 0;
                }


                currBall = Instantiate<Rigidbody>(cream, ballPosition);
                currBall.transform.localPosition = Vector3.zero;
                currBall.transform.forward = this.transform.forward;
                currBall.tag = "enemy";
                currBall.isKinematic = true;
            }
        }

        // Fire
        if (Time.time - timeTracker > firingRate)
        {
            anim.SetBool("isFiring", true);
            if (currBall != null)
            {
                hasCream = false;
                currBall.transform.parent = null;
                currBall.isKinematic = false;
                currBall.velocity = Vector3.zero;
                currBall.angularVelocity = Vector3.zero;

                currBall.AddForce(this.transform.forward * bulletSpeed, ForceMode.VelocityChange);
                currBall = null;
            }
            timeTracker = Time.time;        // Reset firing time
        }
        else if (Time.time - timeTracker > 1)       // Allow time for animation to reset - reload animation is 1 second
        {
            anim.SetBool("isFiring", false);
        }
    }

    private void Shoot()
    {
        source.PlayOneShot(shot_sound, 0.5f);
    }

    private void ICDeath()
    {
        source.PlayOneShot(death_sound, 0.5f);
    }
}

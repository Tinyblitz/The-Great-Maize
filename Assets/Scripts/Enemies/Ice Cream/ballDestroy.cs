using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AudioSource))]
public class ballDestroy : MonoBehaviour
{
    private float prevTime;
    private bool isDestroyed;

    private AudioSource source;
    public AudioClip splat;

    void Start()
    {
        source = GetComponent<AudioSource>();

        prevTime = Time.time;
        isDestroyed = false;
    }

    void Update()
    {
        if (Time.time - prevTime > 10)
        {
            isDestroyed = true;       // Incase projectile leaves playing area, prevents it existing indefinitely using a time limit
        }

        if (isDestroyed && Time.time - prevTime > 0.15)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            source.PlayOneShot(splat, 2.0f);
        }

        if (c.gameObject.tag != "enemy")
        {
            isDestroyed = true;
        }
        prevTime = Time.time;
    }
}

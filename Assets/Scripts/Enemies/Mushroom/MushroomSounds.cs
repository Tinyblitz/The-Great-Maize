using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSounds : MonoBehaviour
{
    private AudioSource source;
    public AudioClip cry;
    public AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void MushCry()
    {
        source.PlayOneShot(cry, 0.5f);
    }

    private void MushDeath()
    {
        source.PlayOneShot(deathSound, 1.0f);
    }
}

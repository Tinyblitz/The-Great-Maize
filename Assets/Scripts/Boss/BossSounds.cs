using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    public AudioClip roar;
    public AudioClip step;
    public AudioClip dmg;
    public AudioClip deathSound;

    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Roar()
    {
        source.PlayOneShot(roar, 1.0f);
    }

    private void Step()
    {
        source.PlayOneShot(step, 1.0f);
    }

    private void DamageSound()
    {
        source.PlayOneShot(dmg, 1.0f);
    }

    private void BossDeathSound()
    {
        source.PlayOneShot(deathSound, 1.0f);
    }
}

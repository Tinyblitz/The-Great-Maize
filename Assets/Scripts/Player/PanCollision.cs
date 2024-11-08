using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCollision : MonoBehaviour
{
    private Animator anim;
    private AudioSource audio;

    void Start()
    {
        anim = PlayerControlScript.instance.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "enemy")
        {
            if (anim.GetBool("isAttacking")) {              // Prevents constant hitbox of weapon
                
                EnemyDeath call = other.GetComponent<EnemyDeath>();
                if (call != null)
                {
                    call.TakeDamage(1);
                    audio.Play();
                }
                else if (other.gameObject.GetComponent<ExplosionExpand>() == null)
                {
                    Destroy(other.gameObject);
                }
            }
        }
        else if (other.tag == "hitbox")
        {

            if (anim.GetBool("isAttacking"))
            {
                GameObject mainBody = GameObject.Find("Boss");

                mainBody.GetComponent<BossDeath>().TakeDamage(1);
                audio.Play();


            }

        }
    }
}

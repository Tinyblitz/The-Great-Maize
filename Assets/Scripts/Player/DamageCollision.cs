using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollision : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {


            //collision.gameObject.GetComponent<EnemyDeath>().TakeDamage(1);
            


            Debug.Log("Enemy hit");
            // Animator enemyAnim = collision.gameObject.GetComponent<Animator>();
            // if (enemyAnim == null)
            // {
            //     enemyAnim = collision.gameObject.GetComponentInChildren<Animator>();
            // }

            // enemyAnim.SetBool("Dead", true);

        }
        else if (collision.gameObject.CompareTag("hitbox"))
        {

 
                GameObject mainBody = GameObject.Find("Boss");

                mainBody.GetComponent<BossDeath>().TakeDamage(1);

            Debug.Log("Enemy hit");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy") {

            Animator enemyAnim = other.gameObject.GetComponent<Animator>();

            if (enemyAnim != null) {

                EnemyDeath call = other.GetComponent<EnemyDeath>();
                if (call != null)
                {
                    call.TakeDamage(1);
                }

            }
            else if (other.gameObject.GetComponent<ExplosionExpand>() == null){
                Destroy(other.gameObject);
            }
        }
        else if (other.tag == "hitbox")
        { 
                GameObject mainBody = GameObject.Find("Boss");

                mainBody.GetComponent<BossDeath>().TakeDamage(1);

        }
    }

}
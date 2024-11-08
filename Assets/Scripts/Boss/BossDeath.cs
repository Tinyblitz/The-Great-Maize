using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    private Animator anim;

    // control enemy health in here since all enemies have the health feature and death feature
    public int health;

    private int dmg;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        if (anim.GetBool("Dead"))
        {

            if (this.transform.GetChild(0).localScale.x == 0)
            {
                Destroy(this.gameObject);
                EnemyCounter.instance.enemiesDefeated += 1;
            }
        }

        health -= dmg;
        dmg = 0;

        if (health <= 0)
        {
            anim.SetBool("Dead", true);
            this.GetComponent<Boss_AI>().aiState = Boss_AI.AIState.Idle;
        }

    }


    public void TakeDamage(int damage)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Roar") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Roll") && !anim.GetCurrentAnimatorStateInfo(0).IsName("BallMode") && !anim.GetCurrentAnimatorStateInfo(2).IsName("Damaged"))
        {
            anim.SetBool("isDamaged", true);
            dmg = damage;
        }
    }
}

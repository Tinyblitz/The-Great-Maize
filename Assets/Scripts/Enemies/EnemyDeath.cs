using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Animator anim;

    private bool isDamaged;
    private int dmg;

    private float timeTracker;

    public float invincibilityTime;

    // control enemy health in here since all enemies have the health feature and death feature
    public int health;

    //set what pickup an enemy spawns upon death
    public int pickupType;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isDamaged = false;
        dmg = 0;

        timeTracker = Time.time;
    }

    void Update()
    {
        if (anim.GetBool("Dead"))
        {

            if (this.transform.localScale.x == 0)
            {
                switch (pickupType)
                {
                    case 0:
                        foreach (GrainPickup pickup in FindObjectsOfType<GrainPickup>(true))
                        {
                            if (!pickup.gameObject.activeSelf)
                            {
                                pickup.transform.position = new Vector3(gameObject.transform.position.x, .5f, gameObject.transform.position.z);
                                pickup.gameObject.SetActive(true);
                                break;
                            }
                        }
                        break;
                    case 1:
                        foreach (VegPickup pickup in FindObjectsOfType<VegPickup>(true))
                        {
                            if (!pickup.gameObject.activeSelf)
                            {
                                pickup.transform.position = new Vector3(gameObject.transform.position.x, .5f, gameObject.transform.position.z);
                                pickup.gameObject.SetActive(true);
                                break;
                            }
                        }
                        break;
                    case 2:
                        foreach (DairyPickup pickup in FindObjectsOfType<DairyPickup>(true))
                        {
                            if (!pickup.gameObject.activeSelf)
                            {
                                pickup.transform.position = new Vector3(gameObject.transform.position.x, .5f, gameObject.transform.position.z);
                                pickup.gameObject.SetActive(true);
                                break;
                            }
                        }
                        break;
                }
                Destroy(this.gameObject);
                EnemyCounter.instance.enemiesDefeated += 1;
            }
        }

        if (isDamaged)
        {
            health -= dmg;
            isDamaged = false;
            dmg = 0;
        }
        

        if (health <= 0)
        {
            anim.SetBool("Dead", true);
            Collider m_collider = GetComponent<Collider>();
            m_collider.enabled = false;
        }
    }

    
    public void TakeDamage(int damage)
    {
        if (Time.time - timeTracker > invincibilityTime)
        {
            isDamaged = true;
            dmg = damage;

            timeTracker = Time.time;
        }
        

        anim.SetBool("isDamaged", true);

    }

}

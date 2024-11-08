using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(UnityEngine.Animator))]
public class PCakeControlScript : MonoBehaviour
{

    public float attackRate;            // In Seconds
    private float timeTracker;

    private NavMeshAgent navAgent;
    private Rigidbody rb;
    private Animator anim;

    private AudioSource source;
    public AudioClip slamSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public enum AIState {
        idle,
        seekTarget
    };

    public AIState aiState;
    public GameObject target;

    public float knockbackAmt;
    public float knockbackDecay = 1f;

    private GameObject explosion;
    public GameObject explosionPrefab;

    // temp variable used to view speed during gameplay
    public Vector3 forces;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
  
        if (navAgent == null)
            Debug.Log("NavMeshAgent could not be found");

        rb = GetComponent<Rigidbody>();
  
        if (rb == null)
            Debug.Log("RigidBody could not be found");
            
        anim = GetComponent<Animator>();
  
        if (anim == null)
            Debug.Log("Animator could not be found");

        source = GetComponent<AudioSource>();

        if (source == null)
            Debug.Log("Audio Source could not be found");

        target = GameObject.FindGameObjectWithTag("Player");
    }


    // Start is called before the first frame update
    void Start()
    {
        aiState = AIState.seekTarget; 
        timeTracker = Time.time - attackRate;

        navAgent.isStopped = true;

    }

    // Update is called once per frame
    void Update()
    {

        // decay the knockback force
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, knockbackDecay * Time.deltaTime);
        forces = rb.velocity;

        // while dying, it stays in place
        if (anim.GetBool("Dead") == true){
            navAgent.isStopped = true;
            return;
        }

        // controls the timed attacks and hops of the pound cake
        if (Time.time - timeTracker > attackRate) {
            timeTracker = Time.time;        // Reset attack timer

            navAgent.isStopped = false;
            navAgent.SetDestination(target.transform.position);

            anim.SetBool("Hopping", true);

        }
        else if (Time.time - timeTracker > 0.95) {
            
            navAgent.isStopped = true;
        }

        // once the agent is done hopping, generate the shockwave attack
        if (anim.GetBool("Hopping") == true && anim.GetCurrentAnimatorStateInfo(0).IsName("Rest"))
        {
            explosion = Instantiate<GameObject>(explosionPrefab);
            explosion.transform.localPosition = transform.position;
            Destroy(explosion, 4);
            anim.SetBool("Hopping", false);
        }

        // switch statement to control state machine
        // state machine in case we add more states (like if you can't see the target)
        switch (aiState)
        {
            // track the target and roll to current position            
            case AIState.seekTarget:
                navAgent.SetDestination(target.transform.position);
                break;

            case AIState.idle:
                // after knockback is mostly finished, start tracking the player again
                // Without this, the ball looks glitchy as the NavMeshAgent fights the knockback
                if ((Math.Abs(rb.velocity.x) + Math.Abs(rb.velocity.z)) <= knockbackAmt * 0.6f) {
                    aiState = AIState.seekTarget;
                    navAgent.isStopped = false;
                }
                break;
        }

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Damaged"))
        {
            anim.SetBool("isDamaged", false);
            transform.Translate(-Vector3.forward * 0.1f);
        }
    }
 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {


            // apply knockback in opposite direction of collision
            // gives the player some breathing room after getting hit
            rb.velocity -= transform.forward * knockbackAmt; 

            // stop the cabbage from manually moving while it is under knockback influence
            aiState = AIState.idle;
            navAgent.isStopped = true;
        }  
    }

    private void Slam()
    {
        source.PlayOneShot(slamSound, 0.75f);
    }

    private void PCJump()
    {
        source.PlayOneShot(jumpSound, 0.3f);
    }

    private void PCDeath()
    {
        source.PlayOneShot(deathSound, 2.0f);
    }
}


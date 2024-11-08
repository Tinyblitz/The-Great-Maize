using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(UnityEngine.Animator))]
public class CabbageControlScript : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private Rigidbody rb;
    private Animator anim;
    private AudioSource source;
    public AudioClip cabDeath;

    public enum AIState {
        idle,
        seekTarget
    };

    public AIState aiState;
    public GameObject target;

    public float knockbackAmt;
    public float knockbackDecay = 1f;

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

        target = GameObject.FindGameObjectWithTag("Player");
    }


    // Start is called before the first frame update
    void Start()
    {
        aiState = AIState.seekTarget;
        anim = this.gameObject.GetComponent<Animator>();
        source = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // decay the knockback force
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, knockbackDecay * Time.deltaTime);

        forces = rb.velocity;



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

        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Damaged") || anim.GetCurrentAnimatorStateInfo(1).IsName("Dead"))
        {
            if (anim.GetBool("isDamaged"))
            {
                rb.velocity -= transform.forward * knockbackAmt;
            }
            anim.SetBool("isDamaged", false);
            
            aiState = AIState.idle;
            navAgent.isStopped = true;
        }
    }
 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            // apply knockback in opposite direction of collision
            // gives the player some breathing room after getting hit
            rb.velocity -= transform.forward * knockbackAmt; 

            // stop the cabbage from manually moving while it is under knockback influence
            aiState = AIState.idle;
            navAgent.isStopped = true;
        }   
    }

    private void CabCry()
    {
        source.PlayOneShot(cabDeath, 2.0f);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(UnityEngine.Animator))]
public class Mushroom : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Animator anim;

    private AudioSource source;
    public AudioClip cry;
    public AudioClip deathSound;

    private float timeTracker;
    private Vector3 targetDestination;

    public float smoothingTimeFactor = 0.5f;
    private Vector3 smoothingParamVel;

    public enum AIState { Idle, Chase, Attack };
    public AIState aiState;
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        anim = gameObject.GetComponent<Animator>();

        source = GetComponent<AudioSource>();

        aiState = AIState.Chase;

        timeTracker = Time.time;

        target = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Damaged"))
        {
            anim.SetBool("isDamaged", false);
            navAgent.isStopped = true;
            transform.Translate(-Vector3.forward * 0.1f);
            
            GetComponent<Collider>().enabled = false;           // Prevent Mushroom from doing dmg while being dmged
        }
        else
        {
            navAgent.isStopped = false;
            GetComponent<Collider>().enabled = true;
        }

        if (anim.GetBool("Dead"))
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
            return;
        }

        float dist = (target.transform.position - this.transform.position).magnitude;
        if (dist < 3)
        {
            if (aiState != AIState.Attack)
            {
                timeTracker = Time.time;
            }
            aiState = AIState.Attack;
            navAgent.isStopped = true;

        }
        else
        {
            aiState = AIState.Chase;
            navAgent.isStopped = false;
        }

        switch (aiState)
        {
            case AIState.Idle:
                break;
            case AIState.Chase:
                anim.SetBool("isAttacking", false);

                

                if (!navAgent.pathPending)
                {
                    
                    float lookAheadT = dist / navAgent.speed;
                    //lookAheadT = Mathf.Clamp(lookAheadT, 0, 2.0f);
                    Vector3 futureTarget = target.transform.position + lookAheadT * target.GetComponent<VelocityReporter>().velocity;
                    targetDestination = futureTarget;
                    
                    navAgent.SetDestination(targetDestination);
                }

                

                break;
            case AIState.Attack:

             
                anim.SetBool("isAttacking", true);
                
                // Rotate with target to make sure hit
                float rotateSpeed = 10.0f;
                Vector3 rotateDirection = target.transform.position - transform.position;

                // The step size is equal to rotation speed times frame time.
                float singleStep = rotateSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                transform.rotation = Quaternion.LookRotation(newDirection);
                break;
        }
        

        anim.SetFloat("vely", navAgent.velocity.magnitude / navAgent.speed);

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

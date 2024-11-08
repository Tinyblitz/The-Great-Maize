using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class ballMode : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navAgen;
    private Animator anim;
    private Rigidbody rb;
    private Vector3 targetDestination;

    private bool hitPlayer = false;

    private Vector3 tarPrevPos;
    private Vector3 targetVelocity = Vector3.zero;
    public float smoothingTimeFactor = 0.5f;
    private Vector3 smoothingParamVel;

    private float timeTracker;

    public GameObject target;

    public Rigidbody bodyPrefab;

    public GameObject mainBody;

    public float ballTime = 15;
    void Start()
    {
        navAgen = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");
        mainBody = GameObject.Find("Boss");
        mainBody.SetActive(false);

        timeTracker = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        float dist = (target.transform.position - this.transform.position).magnitude;
        if (!hitPlayer)
        {
            
            Vector3 rawVelocity = (target.transform.position - tarPrevPos) / Time.deltaTime;
            targetVelocity = Vector3.SmoothDamp(targetVelocity, rawVelocity, ref smoothingParamVel, smoothingTimeFactor);
            tarPrevPos = target.transform.position;
            float lookAheadT = dist / navAgen.speed;
            lookAheadT = Mathf.Clamp(lookAheadT, 0, 2.0f);
            Vector3 futureTarget = target.transform.position + lookAheadT * targetVelocity;
            targetDestination = futureTarget;
            navAgen.SetDestination(targetDestination);
        }
        else if (ballTime != 1)
        {
            ballTime = 1;
            timeTracker = Time.time;
        }

        if (Time.time - timeTracker >= ballTime)
        {
            changeBack();
        }

    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            hitPlayer = true;
            navAgen.isStopped = true;
            navAgen.ResetPath();

            rb = GetComponent<Rigidbody>();
            //rb.isKinematic = false;
            //rb.AddForce(transform.forward * navAgen.speed * 10);
        }
      
    }

    void changeBack()
    {
        mainBody.SetActive(true);
        mainBody.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 2, this.transform.position.z);
        mainBody.transform.forward = this.transform.forward;
        Destroy(this.gameObject);

        mainBody = null;
    }
}

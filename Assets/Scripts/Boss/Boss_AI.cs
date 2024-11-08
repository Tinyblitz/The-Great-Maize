using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(UnityEngine.Animator))]
public class Boss_AI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navAgen;
    private Animator anim;
    private Vector3 targetDestination;

    // Velocity Variables
    private float dist;

    public float smoothingTimeFactor = 0.5f;
    private Vector3 smoothingParamVel;


    // Ball Mode variables
    private Rigidbody currBody;

    private float ballSpeed = 1.0f;

    private float timeTracker;
    public float ballSpeedChangeRate = 1.0f;       // in seconds
    public float ballSpeedMax = 10.0f;
    public float ballSpeedGrowth = 1.0f;
    public float ballChangeRatio = 4.0f;     // How fast is the resulting ball after change

    // Distance Variables
    public float attackDist;
    public float lungeDist;

    //Other Public Variables
    public enum AIState { Idle, Chase, Attack, Lunge, BallMode, Roar, Dead };
    public AIState aiState;
    public GameObject target;

    //Attachments
    private Rigidbody rb;
    public Rigidbody ballPrefab;
    public Rigidbody cabbagePrefab;
    public Rigidbody mushroomPrefab;
    public Rigidbody iceCreamPrefab;
    public Rigidbody poundCakePrefab;

    public GameObject[] spawnPoints;

    //State Variables
    private bool canSpawnAllies = false;
    private int bossHealth;
    private int healthTracker;

    private void Awake()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("No Boss Ball prefab");
        }

        timeTracker = Time.time;
    }
    void Start()
    {
        navAgen = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        anim = gameObject.GetComponent<Animator>();


        //aiState = AIState.Idle;
        //aiState = AIState.Chase;
        //aiState = AIState.Attack;
        //aiState = AIState.Lunge;
        //aiState = AIState.BallMode;
        aiState = AIState.Roar;

        //anim.SetBool("isWalking", true);

        target = GameObject.FindGameObjectWithTag("Player");
        bossHealth = this.gameObject.GetComponent<BossDeath>().health;
        healthTracker = bossHealth;
    }

    void Update()
    {
        dist = (target.transform.position - this.transform.position).magnitude;

        if (anim.GetCurrentAnimatorStateInfo(2).IsName("Damaged"))
        {
            anim.SetBool("isDamaged", false);
        }
        bossHealth = this.gameObject.GetComponent<BossDeath>().health;
        if (healthTracker - bossHealth > 3)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isPrepStrike", false);
            anim.SetBool("isStriking", false);
            anim.SetBool("lungeMode", false);
            anim.SetBool("isRoaring", false);
            aiState = AIState.BallMode;

            healthTracker = bossHealth;
        }

        if (anim.GetBool("Dead"))
        {
            aiState = AIState.Dead;
        }

        switch (aiState)
        {
            case AIState.Idle:
                anim.SetBool("isWalking", false);
                anim.SetBool("ballMode", false);
                anim.SetBool("isPrepStrike", false);
                anim.SetBool("isStriking", false);
                anim.SetBool("lungeMode", false);
                anim.SetBool("isRoaring", false);

                navAgen.isStopped = true;
                navAgen.ResetPath();

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    aiState = AIState.Chase;
                }
                break;
            case AIState.Chase:
                anim.SetBool("isWalking", true);

                if (dist < attackDist)
                {
                    anim.SetBool("isWalking", false);
                    aiState = AIState.Attack;

                    navAgen.isStopped = true;
                    navAgen.ResetPath();
                }
                else if (!navAgen.pathPending)
                {
                    navAgen.SetDestination(FindDestination(navAgen.speed, true));
                }
                break;
            case AIState.Attack:

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    anim.SetBool("isPrepStrike", true);
                    timeTracker = Time.time;
                }
                
                if (dist >= attackDist && !anim.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
                {
                    anim.SetBool("isPrepStrike", false);
                    anim.SetBool("isStriking", false);
                    aiState = AIState.Chase;
                }

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && Time.time - timeTracker <= 0.4)
                {
                    anim.SetBool("isStriking", false);
                    // Rotate with target to make sure hit
                    float rotateSpeed = 10.0f;
                    Vector3 rotateDirection = target.transform.position - transform.position;

                    // The step size is equal to rotation speed times frame time.
                    float singleStep = rotateSpeed * Time.deltaTime;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, rotateDirection, singleStep, 0.0f);

                    // Draw a ray pointing at our target in
                    Debug.DrawRay(transform.position, newDirection, Color.red);

                    transform.rotation = Quaternion.LookRotation(newDirection);
                }

                if (Time.time - timeTracker >= 0.1)
                {
                    anim.SetBool("isStriking", true);
                }

                break;
            case AIState.Lunge:  // Scrapped
                break;
            case AIState.BallMode:
                anim.SetBool("ballMode", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
                {
                    // Morph into Ball
                    if (ballSpeed >= ballSpeedMax)
                    {
                        currBody = Instantiate(ballPrefab);
                        currBody.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
                        Animator ballAnim = currBody.gameObject.GetComponent<Animator>();
                        ballAnim.SetFloat("rollSpeed", ballSpeed/ballChangeRatio);

                        currBody = null;

                        aiState = AIState.Roar;
                        anim.SetBool("ballMode", false);
                        ballSpeed = 1.0f;
                    }
                    // Increase ball speed
                    else if (Time.time - timeTracker >= ballSpeedChangeRate)
                    {
                        timeTracker = Time.time;
                        ballSpeed += ballSpeedGrowth;

                        anim.SetFloat("rollSpeed", ballSpeed);

                    }
                }
                else
                {
                    timeTracker = Time.time;
                }

                break;
            case AIState.Roar:

                anim.SetBool("isRoaring", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Roar"))
                {
                    canSpawnAllies = true;
                }
                else if (canSpawnAllies)
                {
                    canSpawnAllies = false;
                    aiState = AIState.Idle;

                    

                    for (int i = 0; i < spawnPoints.Length; i++)
                    {
                        EnemyCounter.instance.enemiesToDefeat += 1;

                        // Randomize enemy spawns
                        // 0 = Cabbage
                        // 1 = Mushroom
                        // 2 = Ice Cream
                        // 3 = Pound Cake

                        float randomEnemy = 4.0f;

                        while (randomEnemy == 4.0f)  // Avoid the rare case that the random # generator returns 4.0
                        {

                            randomEnemy = Mathf.Floor(Random.Range(0f, 4.0f));

                        }

                        if (randomEnemy == 0f)
                        {
                            rb = Instantiate(cabbagePrefab);
                            rb.transform.position = spawnPoints[i].transform.position;
                            rb = null;
                        }
                        else if (randomEnemy == 1.0f)
                        {
                            rb = Instantiate(mushroomPrefab);
                            rb.transform.position = spawnPoints[i].transform.position;
                            rb = null;
                        }
                        else if (randomEnemy == 2.0f)
                        {
                            rb = Instantiate(iceCreamPrefab);
                            rb.transform.position = new Vector3(spawnPoints[i].transform.position.x, spawnPoints[i].transform.position.y + 0.7f, spawnPoints[i].transform.position.z);
                            rb = null;
                        }
                        else if (randomEnemy == 3.0f)
                        {
                            rb = Instantiate(poundCakePrefab);
                            rb.transform.position = spawnPoints[i].transform.position;
                            rb = null;
                        }
                    }
                }
                break;
            case AIState.Dead:
                anim.SetBool("isWalking", false);
                anim.SetBool("ballMode", false);
                anim.SetBool("isPrepStrike", false);
                anim.SetBool("isStriking", false);
                anim.SetBool("lungeMode", false);
                anim.SetBool("isRoaring", false);

                navAgen.isStopped = true;
                navAgen.ResetPath();
                
                break;
        }

        
    }

    // Predict future position of target
    Vector3 FindDestination(float refSpeed, bool clamp)
    {
        float lookAheadT = dist / refSpeed;
        if (clamp)
        {
            lookAheadT = Mathf.Clamp(lookAheadT, 0, 2.0f);
        }
        Vector3 futureTarget = target.transform.position + lookAheadT * target.GetComponent<VelocityReporter>().velocity;

        return futureTarget;
    }

    
}

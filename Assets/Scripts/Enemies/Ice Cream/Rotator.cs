using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject target;

    // Angular speed in radians per sec.
    public float speed = 1.0f;
    public float smoothingTimeFactor = 0.5f;
    private Vector3 smoothingParamVel;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");


    }
    void Update()
    {
        if (target == null)
        {
            Debug.LogError("No Target Added");
        }

        // Calculate future position of target
       
        float dist = (target.transform.position - this.transform.position).magnitude;
        float lookAheadT = dist / this.GetComponent<BallShooter>().bulletSpeed;
        lookAheadT = Mathf.Clamp(lookAheadT, 0, 1.0f);
        Vector3 futureTarget = target.transform.position + lookAheadT * target.GetComponent<VelocityReporter>().velocity;
        futureTarget.y = transform.position.y;

        // Determine which direction to rotate towards
        Vector3 targetDirection = futureTarget - transform.position;

        if (targetDirection != Vector3.zero)
        {

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object

            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }
    
}

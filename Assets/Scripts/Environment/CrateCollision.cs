using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollision: MonoBehaviour
{
    private float cratesSoound = 4.0f;

    void OnCollisionEnter(Collision c)
    {
        // check the time to play is ... seconds
        // unity time starts (5s) : time.time >= the time loading, do not run the sound at very begin of tha game

        if (c.impulse.magnitude > 0.25f && Time.time > cratesSoound)
        {
            //we'll just use the first contact point for simplicity
            EventManager.TriggerEvent<CratesCollisionEvent, Vector3, float>(c.contacts[0].point, c.impulse.magnitude);
        }
    }
}

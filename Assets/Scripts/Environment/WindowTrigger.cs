using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour
{
    Animator anim;

    public float maxTime = 10.0f;
    public float minTime = 1.0f;
    private float time;
    private float randomTime;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        time = minTime;
        setRandomTime();
    }

    // Update is called once per frame
    void OnTriggerExit(Collider c)
    {
        //Counts up
        time += Time.deltaTime;

        //Check if its the time to trigger and reset the time
        if (time >= randomTime)
        {
            anim.Play("Idle", 0, 0);
            anim.gameObject.SetActive(false);
            setRandomTime();
        } else
        {
            anim.Play("LeftDoor", 0, 0);
            anim.gameObject.SetActive(true);
            setRandomTime();
        }
    }


    void setRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
    } 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    Animator doorAnim;
    bool isOpen;

    void Start()
    {
        doorAnim = GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            isOpen = true;
            doorsController("DoorOpen");
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if (isOpen)
            {
                isOpen = false;
                doorsController("DoorClose");
            }
        }
    }

    void doorsController(string controller)
    {
        doorAnim.SetTrigger(controller);
    }


}

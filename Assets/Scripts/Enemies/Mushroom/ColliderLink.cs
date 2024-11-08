using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLink : MonoBehaviour
{
    private Collider parentCollider = null;
    private GameObject currentObject;

    void Start()
    {
        currentObject = this.gameObject;
        // Find the parent collider
        while(parentCollider == null)
        {
            if (currentObject == currentObject.transform.root.gameObject) return;
            currentObject = currentObject.transform.parent.gameObject;
            parentCollider = currentObject.GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When parent collider is disabled, disable child collider
        if (!parentCollider.enabled)
        {
            this.GetComponent<Collider>().enabled = false;
        }
        else
        {
            this.GetComponent<Collider>().enabled = true;
        }
    }
}

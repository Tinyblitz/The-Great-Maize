using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionExpand : MonoBehaviour
{
    private SphereCollider m_Collider;


    private float t = 0.0f;

    
    void Awake()
    {
        m_Collider = GetComponent<SphereCollider>();
        m_Collider.radius = 0.0f;

        
    }

    // Update is called once per frame
    void Update()
    {
        m_Collider.radius = Mathf.Lerp(0, 4, t);

        t += Time.deltaTime / 3.5f;
        
        // Avoid lingering ring
        if (t >= 1.0f)
        {
            m_Collider.enabled = false;
        }

    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            m_Collider.enabled = false;
        }
    }
}

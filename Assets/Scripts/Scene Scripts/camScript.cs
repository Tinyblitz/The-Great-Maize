using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class camScript : MonoBehaviour
{

    public GameObject player;
    private Vector3 basis;
    private Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        basis = this.transform.position;
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + basis;
        if (playerAnim.GetBool("Dead")) {
           // this.transform position = 
        }
    }
}

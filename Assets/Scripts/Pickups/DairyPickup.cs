using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DairyPickup : MonoBehaviour
{
    private RecipeController recControl;

    void Start()
    {
        recControl = FindObjectOfType<RecipeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            recControl.dairy++;
            this.gameObject.SetActive(false);
        }
    }
}

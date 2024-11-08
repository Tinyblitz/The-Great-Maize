using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int pickupType;
    private RecipeController recControl;

    void Start()
    {
        recControl = FindObjectOfType<RecipeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch(pickupType)
            {
                case 0:
                    recControl.grain++;
                    break;
                case 1:
                    recControl.veg++;
                    break;
                case 2:
                    recControl.dairy++;
                    break;
            }
            this.gameObject.SetActive(false);
        }
    }
}

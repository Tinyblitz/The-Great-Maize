using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouffleRecipe : RecipeClass
{

    private GameObject explosion;
    private float explosionTotal;

    // Start is called before the first frame update
    void Awake()
    {
        vegReq = 1;
        grainReq = 0;
        dairyReq = 1;
        isActive = false;
        isEquipped = false;
        cdCounter = 7f;
        cdTotal = 7f;
        activeCounter = 0f;
        activeTotal = 2f;
        recipeController = GetComponentInParent<RecipeController>();
        model = this.gameObject.transform.GetChild(0).gameObject;
        model.SetActive(false);
        explosion = this.gameObject.transform.GetChild(1).gameObject;
        explosion.SetActive(false);
        explosionTotal = 1f;
        useCount = 0;
    }


    // In charge of active/cd timers
    void Update()
    {
        if (isActive)
        {
            activeCounter += Time.deltaTime;
            if (activeCounter >= activeTotal)
            {
                
                explosion.transform.position = model.transform.position;
                explosion.SetActive(true);
                isActive = false;
                activeCounter = 0f;
                cdCounter = 0f;
                //maybe have animation for end of attack? TO DO LATER
                model.transform.parent = this.transform;
                model.transform.position = this.transform.position;
                model.SetActive(false);
            }
        }
        else if (cdCounter < cdTotal)
        {
            cdCounter += Time.deltaTime;
            if (cdCounter >= explosionTotal)
            {
                explosion.SetActive(false);
            }
        }
    }

    //checks if soufflerecipe can fire. requires 3 states: cooldown full, not active, and enough ingredients to cast
    public override bool canFire()
    {
        if (cdCounter >= cdTotal && !isActive)
        {
            if (recipeController.veg >= vegReq && recipeController.grain >= grainReq && recipeController.dairy >= dairyReq)
            {
                return true;
            }
        }
        return false;
    }

    //fires pizzarecipe, subtracts required ingredients from total
    public override void onFire()
    {
        Debug.Log("FIRED");
        //NOTE: DEF NEED COOKING WINDUP ANIMATION
        isActive = true;
        model.SetActive(true);
        recipeController.veg -= vegReq;
        recipeController.grain -= grainReq;
        recipeController.dairy -= dairyReq;
        model.transform.parent = null;
        useCount++;
    }
}

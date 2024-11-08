using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TofuRecipe : RecipeClass
{
    void Awake()
    {
        vegReq = 1;
        grainReq = 1;
        dairyReq = 0;
        isActive = false;
        isEquipped = true;
        cdCounter = 10f;
        cdTotal = 10f;
        activeCounter = 0f;
        activeTotal = 5f;
        recipeController = GetComponentInParent<RecipeController>();
        model = this.gameObject.transform.GetChild(0).gameObject;
        model.SetActive(false);
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
                isActive = false;
                activeCounter = 0f;
                cdCounter = 0f;
                //maybe have animation for end of attack? TO DO LATER
                model.SetActive(false);
            }
        }
        else if (cdCounter < cdTotal)
        {
            cdCounter += Time.deltaTime;
        }
    }

    //checks if pizzarecipe can fire. requires 3 states: cooldown full, not active, and enough ingredients to cast
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
        useCount++;
    }
}

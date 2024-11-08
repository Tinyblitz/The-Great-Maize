using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRecipe : RecipeClass
{
    public GameObject pizzaOrbitObj;

    void Awake()
    {
        vegReq = 0;
        grainReq = 1;
        dairyReq = 1;
        isActive = false;
        isEquipped = true;
        cdCounter = 10f;
        cdTotal = 10f;
        activeCounter = 0f;
        activeTotal = 5f;
        recipeController = GetComponentInParent<RecipeController>();
        model = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        model.SetActive(false);
        useCount = 0;
        pizzaOrbitObj = this.gameObject.transform.GetChild(0).gameObject;
    }

    // In charge of active/cd timers
    void Update()
    {
        if (isActive)
        {
            pizzaOrbitObj.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
            activeCounter += Time.deltaTime;
            if (activeCounter >= activeTotal)
            {
                isActive = false;
                activeCounter = 0f;
                cdCounter = 0f;
                pizzaOrbitObj.transform.parent = this.transform;
                //maybe have animation for end of attack? TO DO LATER
                model.SetActive(false);
            }
        } else if (cdCounter < cdTotal)
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
        pizzaOrbitObj.transform.parent = null;
    }
}

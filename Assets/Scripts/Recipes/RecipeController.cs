using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeController : MonoBehaviour
{
    public int grain = 10;
    public int veg = 10;
    public int dairy = 10;
    //public List<IRecipe> equippedRec;
    public RecipeClass[] equippedRec;

    public int activeRecipeSlot = 0;
    public static RecipeController instance;
    public bool recipeCast = false;


    public void Awake()
    {
        instance = this;
        equippedRec = new RecipeClass[3];
        equippedRec[0] = GetComponentInChildren<PizzaRecipe>();
        equippedRec[1] = GetComponentInChildren<SouffleRecipe>();
        equippedRec[2] = GetComponentInChildren<TofuRecipe>();
    }
    
    void Update()
    {
        //Test for input, will change w/ merge
        
        if (recipeCast)
        {
            if (equippedRec[activeRecipeSlot].canFire())
            {
                equippedRec[activeRecipeSlot].onFire();
            }
            recipeCast = false;
        }
        /*
        if (Input.GetKeyDown("q"))
        {
            if (equippedRec[0].canFire())
            {
                equippedRec[0].onFire();
            }
        }
        if (Input.GetKeyDown("e"))
        {
            if (equippedRec[1].canFire())
            {
                equippedRec[1].onFire();
            }
        }
        */
    }
}

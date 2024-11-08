using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeClass : MonoBehaviour
{
    public bool isEquipped;
    public bool isActive;
    public int vegReq;
    public int grainReq;
    public int dairyReq;
    public float cdTotal;
    public float cdCounter;
    public int upgradeCount;
    public RecipeController recipeController;
    public float activeTotal;
    public float activeCounter;
    protected GameObject model;
    public int useCount;

    public virtual void onFire()
    {
        //to be overwritten and implemented by other recipes
        Debug.Log("WRONG METHOD");
    }

    public virtual bool canFire()
    {
        //to be overwritten and implemented by other recipes
        Debug.Log("BASE CLASS METHOD");
        return false;
    }
}
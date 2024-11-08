using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VegAmountScript : MonoBehaviour
{
    public TMP_Text textMesh;
    public GameObject recipeManager;

    void Awake()
    {
        textMesh.SetText("" + recipeManager.GetComponent<RecipeController>().veg);
    }

    void LateUpdate()
    {
        textMesh.SetText("" + recipeManager.GetComponent<RecipeController>().veg);
    }
}
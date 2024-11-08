using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrainAmountScript : MonoBehaviour
{
    public TMP_Text textMesh;
    public GameObject recipeManager;

    void Awake()
    {
        textMesh.SetText("" + recipeManager.GetComponent<RecipeController>().grain);
    }

    void LateUpdate()
    {
        textMesh.SetText("" + recipeManager.GetComponent<RecipeController>().grain);
    }
}
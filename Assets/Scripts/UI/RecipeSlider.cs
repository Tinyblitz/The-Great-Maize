using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlider : MonoBehaviour
{
    public Slider slider;
    public GameObject recipeManager;
    public int recipeNum = 0;
    public GameObject cooldown;

    private RecipeController recipeController;
    private RecipeClass recipe;
    private Color recipeColor;
    private Color inactiveColor = new Color(189f/255f, 195f/255f, 199f/255f, 1.0f);
    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        recipeController = recipeManager.GetComponent<RecipeController>();
        recipe = recipeController.equippedRec[recipeNum];
        slider.maxValue = recipe.cdTotal;
        slider.value = recipe.cdCounter;
        recipeColor = cooldown.GetComponent<Image>().color;
        if (recipeController.activeRecipeSlot != recipeNum) {
            cooldown.GetComponent<Image>().color = inactiveColor;
            active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((recipeController.activeRecipeSlot != recipeNum) && active) {
            cooldown.GetComponent<Image>().color = inactiveColor;
            active = false;
        } else if ((recipeController.activeRecipeSlot == recipeNum) && (!active)) {
            cooldown.GetComponent<Image>().color = recipeColor;
            active = true;
        }


        if (recipe.isActive) {
            slider.value = recipe.cdTotal - (recipe.activeCounter * (recipe.cdTotal / recipe.activeTotal));
        } else {
            slider.value = recipe.cdCounter;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class FroggieOven : MonoBehaviour
{
    /// <summary>
    public GameObject[] Ingredients; // Make it the size of 2.
    public GameObject IngredientSlotA;
    public GameObject IngredientSlotB;

    public Inventory inventory;

    /// </summary>

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    public void CookFrog()
    {
     //Check both ingredients

    //Check if ingrediants are any of the recipes

    //If ingrediants match -> Add Frog to playable characters

    }

    public void AddIngredient(GameObject ThisInventorySlot)
    {
        bool canAdd = false;
        GameObject slotToAdd;

        // Check if ingredient Slots are empty
        foreach(GameObject ingredient in Ingredients)
        {
            if(ingredient == null)
            {
                canAdd = true;
                slotToAdd = ingredient;
                break;
            }
        }

        // Find the ingredient to add to the Slot
        
        // Add ingredient to Array

        // Add Ingredient to empty slot
    }

    public void RemoveIngredient()
    {

    }

}

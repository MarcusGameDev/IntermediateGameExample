using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Added this to access the "Button" component

public class Inventory : MonoBehaviour
{
    //Objects in your Invetory
    public List<GameObject> Items;

    //Inventory slots in the UI
    public List<GameObject> InventorySlots;

    public GameObject InventoryUI;
    public Image defaultButtonImage;

    //Crafting System Variables
    public List<GameObject> Ingredients;
    public List<GameObject> IngredientSlots;
    public List<Craftable> CraftablePrefabs; //Script Reference for Craftable Items

    public void Start()
    {
        InventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item") && Items.Count < InventorySlots.Count)
        {
            AddItem(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(InventoryUI.activeSelf == false && Input.GetKeyDown(KeyCode.E))
        {
            InventoryUI.SetActive(true);
        }
        else if (InventoryUI.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            InventoryUI.SetActive(false);
        }
    }

    void AddItem(GameObject item)
    {
        Items.Add(item);

        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        //Go through each Item in the list. Set the Item's Image to be the equilivant "Inventory UI" Image.
        for(int i = 0; i < InventorySlots.Count; i++)
        {
            if (Items.Count > i && Items[i] != null)
            {
                InventorySlots[i].GetComponent<Image>().sprite = Items[i].GetComponent<Image>().sprite;
              //  InventorySlots[i].SetActive(true);
            }
            else
            {
                InventorySlots[i].GetComponent<Image>().sprite = defaultButtonImage.sprite;
            }
        }
    }

    public void RemoveItem(GameObject ThisInventorySlot)
    {
        //Check if there is an item to remove
        if (Items.Count > 0)
        {
            //Match this inventorySlot with the list
            int slotToRemove = 0;

            foreach (GameObject inventorySlot in InventorySlots)
            {
                if (inventorySlot == ThisInventorySlot)
                {
                    //Exits check once it finds the same item in the inventory
                    break;
                }
                slotToRemove++;
            }

            // Check if the ingredients are free. If they can add ingredient to ingredient slot, then remove at the appropriate slot.
            if(AddIngredient(Items[slotToRemove]) == true)
            {
                //Remove the itemSlot that matches the Slot to remove
            //    Debug.Log("Inventory Removed" + Items[slotToRemove].name);
                Items.RemoveAt(slotToRemove);
           
            }

            UpdateInventoryUI();
        //    Debug.Log("UI Updated");
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Crafting System

    bool AddIngredient(GameObject inventoryToAdd)
    {
        for(int i = 0; i < Ingredients.Count; i++)
        {
            if (Ingredients[i] == null)
            {
                // Add ingredient to empty slot
                Ingredients[i] = inventoryToAdd;

                //Update Ingredients UI once ingredient has been added.
                UpdateIngredientsUI();
                Debug.Log("Ingredient Added");
                return true; // Return true if added item to ingredients.
            }
        }
        return false; // If no ingredient was added to the list.
    }

    void UpdateIngredientsUI()
    {
        //Go through each Item in the list. Set the Item's Image to be the equilivant "Inventory UI" Image.
        for (int i = 0; i < IngredientSlots.Count; i++)
        {
            //if (Ingredients.Count > i && Ingredients[i] != null)
            if(Ingredients.Count > i && Ingredients[i] != null)
            {
              IngredientSlots[i].GetComponent<Image>().sprite = Ingredients[i].GetComponent<Image>().sprite;
                //  InventorySlots[i].SetActive(true);
            }
            else
            {
                IngredientSlots[i].GetComponent<Image>().sprite = defaultButtonImage.sprite;
            }
        }
    }

    //Craft Button
    public void Craft()
    {
        bool canCraft = false;

        // Check if both ingredients match any of the recipes.If they do match set it to start craft process
        for (int i = 0; i < CraftablePrefabs.Count; i++)
        {
            //Check the IMAGE of both to confirm they are the same item (Could use a script/Tag as a different form of identification)
            if (Ingredients[0].GetComponent<Image>().sprite == CraftablePrefabs[i].requiredIngredients[0].GetComponent<Image>().sprite && Ingredients[1].GetComponent<Image>().sprite == CraftablePrefabs[i].requiredIngredients[1].GetComponent<Image>().sprite)
            {
                canCraft = true;
                CraftablePrefabs[i].isActive = true; //Enable the frogsuit
                break;
            }
            else if (Ingredients[0].GetComponent<Image>().sprite == CraftablePrefabs[i].requiredIngredients[1].GetComponent<Image>().sprite && Ingredients[1].GetComponent<Image>().sprite == CraftablePrefabs[i].requiredIngredients[0].GetComponent<Image>().sprite)
            {
                canCraft = true;
                CraftablePrefabs[i].isActive = true; //Enable the frogsuit
                break;
            }
        }

        //If they do match, then remove both ingredients 
        if(canCraft == true)
        {
            for(int i = 0; i < Ingredients.Count; i++)
            {
                RemoveIngredient(i);
            }
        }
        else
        {
            Debug.Log("Ingredients don't match");
            Debug.Log("Ingredients " + Ingredients[0].name + " & " + Ingredients[1].name);
        }
    }

    void RemoveIngredient(int removePosition)
    {
        //Ingredients.Remove(ingredient);

        //Set the value at this list to null. WE DON'T REMOVE AS WE WANT TO KEEP ONLY 2 INGREDIENT SLOTS!!!!
        Ingredients[removePosition] = null;

        UpdateIngredientsUI();
    }

    //Add Public void Return Ingredient, to move ingredients slot back to the inventory slots.
    public void ReturnIngredient(GameObject ThisIngredientSlot)
    {
        //Check if there is enough room in the inventory to add a slot back
        if (Items.Count < InventorySlots.Count)
        {
            int removePosition = 0;

            foreach (GameObject ingredientSlot in IngredientSlots)
            {
                if (ingredientSlot == ThisIngredientSlot)
                {
                    //Exits check once it finds the same item in the inventory
                    break;
                }
                removePosition++;
            }
            if (removePosition <= Ingredients.Count)
            {
                AddItem(Ingredients[removePosition]);
                Debug.Log("Removed: " + Ingredients[removePosition].name);

                RemoveIngredient(removePosition);
            }
        }   
    }
}

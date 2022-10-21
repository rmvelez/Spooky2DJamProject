using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public PlayerController playerController;
    public RectTransform itemUIPrefab;
    public RectTransform inventoryPanel;
    private List<ItemUI> itemUIs = new List<ItemUI>();

    #region Singleton
    private static PlayerUI instance;
    public static PlayerUI GetInstance()
    {
        if (instance == null)
        {
            GameObject playerUIGameObject = GameObject.Find("PlayerUI");
            if (playerUIGameObject != null) instance = playerUIGameObject.GetComponent<PlayerUI>();
            if (instance == null) Debug.LogError("PlayerUI Prefab is missing in the scene!");
        }
        return instance;
    }
    #endregion

    /// <summary>
    /// Updates the amount of lives displayed
    /// </summary>
    public void UpdateLives()
    {

    }


    /// <summary>
    /// Updates the inventory
    /// </summary>
    public void UpdateInventory()
    {
        // Create new Item UIs if needed and adjust sprites and select state
        for (int i = 0; i < playerController.inventory.Count; i++)
        {
            if(itemUIs.Count <= i)
            {
                itemUIs.Add(Instantiate(itemUIPrefab, inventoryPanel).GetComponent<ItemUI>());
            }

            itemUIs[i].itemImage.sprite = playerController.inventory[i].inventoryIcon;

            if (playerController.equippedItem.baseItem == playerController.inventory[i]) 
            {
                itemUIs[i].Select();
            }
            else
            {
                itemUIs[i].Deselect();
            }

        }

        // Delete Item UIs if there are too many (an item was used)
        for (int i = itemUIs.Count - 1; i > playerController.inventory.Count - 1; i--)
        {
            Destroy(itemUIs[i].gameObject);
        }

    }

}

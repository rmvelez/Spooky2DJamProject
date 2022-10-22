using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerController playerController;
    public RectTransform itemUIPrefab;
    public RectTransform heartUIPrefab;
    public Sprite heartFullSprite;
    public Sprite heartHalfSprite;
    
    public RectTransform inventoryPanel;
    public RectTransform heartPanel;

    private List<ItemUI> itemUIs = new List<ItemUI>();
    private List<Image> heartImages = new List<Image>();

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
        float livesToDraw = playerController.nrOfLives;
        int imgIndex = 0;

        while (livesToDraw > 0)
        {
            // Get next image (add new one if needed)
            if (heartImages.Count <= imgIndex)
            {
                Image newHeartImg = Instantiate(heartUIPrefab, heartPanel).GetComponent<Image>();
                heartImages.Add(newHeartImg);
            }

            // Draw a whole heart
            if (livesToDraw >= 1)
            {
                heartImages[imgIndex].sprite = heartFullSprite;
            }
            // Draw half a heart
            else
            {
                heartImages[imgIndex].sprite = heartHalfSprite;
            }

            livesToDraw--;
            imgIndex++;
        }


        // Remove unneeded hearts
        while (heartImages.Count > playerController.nrOfLives + 0.5f)
        {
            Image imageToRemove = heartImages[heartImages.Count - 1];
            heartImages.Remove(imageToRemove);
            Destroy(imageToRemove.gameObject);
        }
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
            ItemUI itemUIToDestroy = itemUIs[i];
            itemUIs.RemoveAt(i);
            Destroy(itemUIToDestroy.gameObject);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory Instance;

    [Header("Inventory Settings")]
    [SerializeField] private int maxSlots = 5;
    [SerializeField] private List<Item> items;

    [Header("UI Settings")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private GameObject slotPrefab;

    public string StopScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        

    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == StopScene)
        {
            Destroy(gameObject);
        }
    }


    public bool AddItem(Item itemToAdd)
    {
        if (items.Count < maxSlots)
        {
            items.Add(itemToAdd);
            UpdateInventoryUI();
            Debug.Log($"Item {itemToAdd.itemName} added to inventory.");
            
            return true;
            
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            UpdateInventoryUI();
            Debug.Log($"Item {itemToRemove.itemName} removed from inventory.");
        }
    }

    public void DisplayInventory()
    {
        Debug.Log("Inventory:");
        foreach (Item item in items)
        {
            Debug.Log($"- {item.itemName}");
        }
    }

    private void UpdateInventoryUI()
    {
        foreach (Transform child in slotsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
            Image icon = slot.transform.GetChild(0).GetComponent<Image>(); // UI.Image
            icon.sprite = item.itemIcon; 
        }
    }

    #region verification Item
    public bool HasItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
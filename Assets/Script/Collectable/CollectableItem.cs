using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("Collect Settings")]
    [SerializeField] private KeyCode collectItem;

    [Header("UI Interaction")]
    [SerializeField] private GameObject interactionUI;

    [Header("Item Settings")]
    [SerializeField] private Item item;
    [SerializeField] private Inventory playerInventory; 

    [Header("Debug")]
    [SerializeField] private bool isPlayerInRange = false;

    private void Start()
    {
        interactionUI.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(collectItem))
        {
            CollectItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision Detected");
            isPlayerInRange = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionUI.SetActive(false);
        }
    }

    private void CollectItem()
    {
        // Rechercher automatiquement l'inventaire si non assigné
        if (playerInventory == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerInventory = player.GetComponent<Inventory>();
            }
        }

        // Vérifiez à nouveau si playerInventory existe
        if (playerInventory != null && playerInventory.AddItem(item))
        {
            Debug.Log($"Collected {item.itemName}");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerInventory is not assigned or item could not be added.");
        }
    }
}
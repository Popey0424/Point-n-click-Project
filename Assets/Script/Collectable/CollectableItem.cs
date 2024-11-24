using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{


    //[Header("UI Interaction")]
    //[SerializeField] private GameObject interactionUI;

    [Header("Item Settings")]
    [SerializeField] private Item item;
    [SerializeField] private Inventory playerInventory;

    [Header("Debug")]
    [SerializeField] private bool isMouseOver = false;
    [SerializeField] private bool isPlayerInRange = false;

    private void Start()
    {
        //interactionUI.SetActive(false);
    }

    //private void Update()
    //{
        //if (isPlayerInRange && Input.GetKeyDown(collectItem))
        //{
        //    CollectItem();
        //}
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("Collision Detected");
    //        isPlayerInRange = true;
    //        interactionUI.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        isPlayerInRange = false;
    //        interactionUI.SetActive(false);
    //    }
    //}

    private void OnMouseEnter()
    {
        Debug.Log("Souris OK");
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        Debug.Log("Souris Pas OK");
        isMouseOver = false;
    }
    private void OnMouseDown()
    {
        if (isMouseOver)
        {
            Debug.Log("Touchée");
            CollectItem();
        }
    }

    private void CollectItem()
    {
      
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerInventory = player.GetComponent<Inventory>();
            }
        }


        if (playerInventory != null && playerInventory.AddItem(item))
        {
            Debug.Log("DEEEEEEEEEEEEEEEEEEEEESTROY");
            Debug.Log($"Collected {item.itemName}");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("dfssef");
        }
    }
}
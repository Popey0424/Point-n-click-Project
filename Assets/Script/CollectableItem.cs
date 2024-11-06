using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("Collect Settigns")]
    [SerializeField] private KeyCode collectItem;

    [Header("UI Interaction")]
    [SerializeField] private GameObject interactionUI;

    [Header("Debug")]
    [SerializeField] private bool isPlayerInRange = false;


    private void Start()
    {
        interactionUI.SetActive(false);
    }
    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(collectItem))
        {
            Debug.Log("Collecter");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision Detecter");
            isPlayerInRange = true;
            interactionUI.SetActive(true);  
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionUI.SetActive(false);  
        }
    }

}

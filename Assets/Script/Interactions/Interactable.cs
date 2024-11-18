using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private string requiredItemName; 
    [SerializeField] private Color highlightColor = Color.white; 
    [SerializeField] private Color defaultColor = Color.white;  

    private SpriteRenderer spriteRenderer;
    private Inventory playerInventory;
    private bool isMouseOver = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer not found on the interactable object.");
        }

       
        spriteRenderer.color = defaultColor;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Souris OK");
        
        isMouseOver = true;
        SetHighlight(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Souris Plus Ok");
      
        isMouseOver = false;
        SetHighlight(false);
    }

    private void OnMouseDown()
    {
       
        if (playerInventory == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerInventory = player.GetComponent<Inventory>();
            }
        }

        if (playerInventory != null && playerInventory.HasItem(requiredItemName))
        {
            Debug.Log($"Interaction réussie avec {gameObject.name} !");
            PerformInteraction();
        }
        else
        {
            Debug.Log($"Interaction échouée : {requiredItemName} manquant.");
        }
    }

    private void SetHighlight(bool enabled)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = enabled ? highlightColor : defaultColor;
        }
    }

    private void PerformInteraction()
    {
 
        Debug.Log($"{gameObject.name} activé !");
     
    }
}
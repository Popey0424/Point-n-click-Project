using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private string requiredItemName; // Nom de l'item requis pour l'interaction
    [SerializeField] private Color highlightColor = Color.white; // Couleur de surbrillance
    [SerializeField] private Color defaultColor = Color.white;  // Couleur par défaut

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

        // Définit la couleur par défaut
        spriteRenderer.color = defaultColor;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Souris OK");
        // La souris passe sur l'objet
        isMouseOver = true;
        SetHighlight(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("Souris Plus Ok");
        // La souris quitte l'objet
        isMouseOver = false;
        SetHighlight(false);
    }

    private void OnMouseDown()
    {
        // Détecter un clic gauche sur l'objet
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
        // Logique spécifique à l'objet (par exemple, ouvrir une porte)
        Debug.Log($"{gameObject.name} activé !");
        // Ajoutez votre logique ici (animation, destruction, etc.)
    }
}
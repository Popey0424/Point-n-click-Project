using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private string requiredItemName; // Nom de l'item requis pour l'interaction
    [SerializeField] private Color highlightColor = Color.white; // Couleur de surbrillance
    [SerializeField] private Color defaultColor = Color.white;  // Couleur par d�faut

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

        // D�finit la couleur par d�faut
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
        // D�tecter un clic gauche sur l'objet
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
            Debug.Log($"Interaction r�ussie avec {gameObject.name} !");
            PerformInteraction();
        }
        else
        {
            Debug.Log($"Interaction �chou�e : {requiredItemName} manquant.");
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
        // Logique sp�cifique � l'objet (par exemple, ouvrir une porte)
        Debug.Log($"{gameObject.name} activ� !");
        // Ajoutez votre logique ici (animation, destruction, etc.)
    }
}
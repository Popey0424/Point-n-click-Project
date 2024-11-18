using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private KeyCode interactionItem;

    [Header("UI Interaction")]
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private Button backInteractionButton;

    [Header("OpenImage")]
    [SerializeField] private GameObject interactionImage;

    [Header("Debug")]
    [SerializeField] private bool isPlayerInRange= false;
    [SerializeField] private bool isImageOpen = false;

    private void Start()
    {
        interactionUI.SetActive(false);
        interactionImage.SetActive(false);
        backInteractionButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(interactionItem) && !isImageOpen)
        {
            isImageOpen = true;
            interactionImage.SetActive(true);
            backInteractionButton.gameObject.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider collision)
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

    public void OnClickBackButton()
    {
        if (isImageOpen)
        {
            interactionImage.SetActive(false);
            backInteractionButton.gameObject.SetActive(false);
            isImageOpen= false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{


 

    [Header("Open/Close Image")]
    [SerializeField] private GameObject interactionImage;
    [SerializeField] private Button backInteractionButton;

    [Header("Debug")]
    [SerializeField] private bool IsMouseIn = false;
    [SerializeField] private bool isPlayerInRange = false;
    [SerializeField] private bool isImageOpen = false;

    private void Start()
    {
      
        interactionImage.SetActive(false);
        backInteractionButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        //if(isPlayerInRange && Input.GetKeyDown(interactionItem) && !isImageOpen)
        //{
        //    isImageOpen = true;
        //    interactionImage.SetActive(true);
        //    backInteractionButton.gameObject.SetActive(true);
        //}

    }

    private void OnMouseEnter()
    {
        Debug.Log("Souris Ok");
        IsMouseIn = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("Souris Pas Ok ");
        IsMouseIn = false;
    }

    private void OnMouseDown()
    {
        if (IsMouseIn)
        {
            while (!isPlayerInRange)
            {
                Debug.Log("Il se depalce vers l'image");
            }
            if (isPlayerInRange)
            {
                Debug.Log("il est dans la frange ouvre l'image");
                OpenImage();
            }
        }
    }

    private void OpenImage()
    {
        isImageOpen = true;
        interactionImage.SetActive(true);
        backInteractionButton.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision Detecter");
            isPlayerInRange = true;
           
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
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

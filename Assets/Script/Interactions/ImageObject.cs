using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageObject : MonoBehaviour
{


    [SerializeField] private int interactionErrorID;

    [Header("Open/Close Image")]
    [SerializeField] private GameObject interactionImage;
    [SerializeField] private Button backInteractionButton;

    [Header("Debug")]
    [SerializeField] private bool IsMouseIn = false;
    [SerializeField] private bool isPlayerInRange = false;
    [SerializeField] private bool isImageOpen = false;

    [Header("Show Error dialog")]
    [SerializeField] private string Charactername;
    [SerializeField] private string TextError;
    [SerializeField] private TextMeshProUGUI textDialogError;
    [SerializeField] private TextMeshProUGUI textCharacterName;
    [SerializeField] private GameObject HUDdialog;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private Image RaycastImage;


    private PlayerMovement1 playerMovement1;

    private void Start()
    {
        RaycastImage.gameObject.SetActive(false);
        interactionImage.SetActive(false);
        backInteractionButton.gameObject.SetActive(true);
        playerMovement1 = FindObjectOfType<PlayerMovement1>();
    }

    private void Update()
    {
        
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
        if (IsMouseIn && isPlayerInRange==false)
        {
            Debug.Log("je dois me rapprocher");
           
           
        }
        else if(IsMouseIn && isPlayerInRange== true)
        {
            Debug.Log("cest bon");
            OpenImage();
        }
    }

    private void OpenImage()
    {
        isImageOpen = true;
        interactionImage.SetActive(true);
        backInteractionButton.gameObject.SetActive(true);
        if (playerMovement1 != null)
        {
            playerMovement1.StopMovement(false);
        }
        DialogError();

    }
    private void OnTriggerEnter2D(Collider2D collision)
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
            if (playerMovement1 != null)
            {
                playerMovement1.StopMovement(true);
            }
        }
    }

    private void DialogError()
    {
        RaycastImage.gameObject.SetActive(true);
        HUDdialog.SetActive(true);
        textDialogError.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);

        if(playerMovement1 != null)
        {
            playerMovement1.StopMovement(false);
            StartCoroutine(TypeLineError());
        }

    }
    private IEnumerator TypeLineError()
    {
        textDialogError.text = "";
        foreach(char letter in TextError)
        {
            textDialogError.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
     
    }
    public void OnClickContinueError()
    {
        Debug.Log("Jai appuyer dessus");
        textDialogError.text = string.Empty;
        HUDdialog.SetActive(false );
        continueButton.SetActive(false);
        textDialogError.gameObject.SetActive(false) ;
        
    }
}

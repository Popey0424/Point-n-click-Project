using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TextDialog
{
    public enum CharacterType { TeddyBear, Enemy, Mother, Sister, Robbie, Scott }

    public CharacterType characterType;
    public string[] TextString; 
    public int InteractionID;   
    public Button ContinueText;
    public GameObject CollisionInteraction;
}

public class DialogManager : MonoBehaviour
{
    [Header("List")]
    [SerializeField] private List<TextDialog> dialogList = new List<TextDialog>();

    [Header("Dialogue Settings")]
    [SerializeField] private GameObject HUDdialog;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private int maxPoints;

    private int points;
    private int currentInteractionId;
    private int textId; 
    private TextDialog currentDialog;
    private PlayerMovement playerMovement;

    private void Start()
    {
        continueButton.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void StartDialogue(int interactionId)
    {
        HUDdialog.SetActive(true);
        textDialog.gameObject.SetActive(true);
        continueButton.SetActive(false);

        if (playerMovement != null)
        {
            playerMovement.StopMovement(false); 
        }

        currentDialog = dialogList.Find(d => d.InteractionID == interactionId);

        if (currentDialog != null)
        {
            textId = 0; 
            StartCoroutine(TypeLine(currentDialog));
        }
    }

    private IEnumerator TypeLine(TextDialog dialog)
    {
        textDialog.text = "";

        foreach (char letter in dialog.TextString[textId])
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    public void OnClickContinueDialog()
    {
        if (currentDialog == null) return;

       
        if (textId < currentDialog.TextString.Length - 1)
        {
            textId++; 
            continueButton.SetActive(false);
            StartCoroutine(TypeLine(currentDialog));
        }
        else
        {
            EndDialogue(); 
        }
    }

    private void EndDialogue()
    {
        HUDdialog.SetActive(false);
        textDialog.text = "";

        if (playerMovement != null)
        {
            playerMovement.StopMovement(true);
        }

        
        if (dialogList.Contains(currentDialog))
        {
            dialogList.Remove(currentDialog);
        }

      
        if (currentDialog.CollisionInteraction != null)
        {
            
            Collider2D collider = currentDialog.CollisionInteraction.GetComponent<Collider2D>();
            if (collider != null)
            {
                Destroy(collider); 
            }

         
            Destroy(currentDialog.CollisionInteraction);
        }

        currentDialog = null; 
    }


    private void OnTeddyBearDialogueEnd()
    {
        Debug.Log("Fin dialogue avec nounours");
    }

    private void OnEnemyDialogueEnd()
    {
        Debug.Log("Enemy mort");
    }
}

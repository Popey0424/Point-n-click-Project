using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TextDialog
{
    public enum CharacterType { TeddyBear, Enemy, Mother, Sister, Robbie, Scott }

    [System.Serializable]
    public class DialogLine
    {
        public CharacterType characterType;
        public string text;
    }

    public List<DialogLine> dialogLines;
    public int InteractionID;
    public Button ContinueText;
    public GameObject CollisionInteraction;
    public bool WantToDestroy;
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
    [SerializeField] private TextMeshProUGUI characterNameText;
   

    private int points;
    private int currentInteractionId;
    private int textId;
    private TextDialog currentDialog;
    private PlayerMovement playerMovement;
    public Chapter1 chapter1;
    public PlayerMovement1 playerMovement1;
    

    private void Start()
    {
        continueButton.SetActive(false);
        playerMovement1 = FindObjectOfType<PlayerMovement1>();
    }

    public void StartDialogue(int interactionId)
    {
        HUDdialog.SetActive(true);
        textDialog.gameObject.SetActive(true);
        continueButton.SetActive(false);

        if (playerMovement1 != null)
        {
            playerMovement1.StopMovement(false);
        }

        if (playerMovement != null)
        {
            playerMovement.StopMovement(false);
        }

        currentDialog = dialogList.Find(d => d.InteractionID == interactionId);

        if (currentDialog != null)
        {
            textId = 0;
            StartCoroutine(TypeLine());
        }
    }

    private IEnumerator TypeLine()
    {
        if (currentDialog == null || textId >= currentDialog.dialogLines.Count) yield break;

        TextDialog.DialogLine line = currentDialog.dialogLines[textId];

       
        characterNameText.text = line.characterType.ToString();
        textDialog.text = "";

        foreach (char letter in line.text)
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    public void OnClickContinueDialog()
    {
        if (currentDialog == null) return;

        if (textId < currentDialog.dialogLines.Count - 1)
        {
            textId++;
            continueButton.SetActive(false);
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        HUDdialog.SetActive(false);
        continueButton.SetActive(false);
        textDialog.text = "";
        characterNameText.text = "";
        if (currentDialog != null && currentDialog.InteractionID == 5)
        {
            Debug.Log("Mort");
            chapter1.EndOfChapterOne();
        }

        if (playerMovement != null)
        {
            playerMovement.StopMovement(true);
        }

        if (playerMovement1 != null)
        {
            playerMovement1.StopMovement(true);
        }

        if (dialogList.Contains(currentDialog))
        {
            dialogList.Remove(currentDialog);
        }

        if (currentDialog.CollisionInteraction != null && currentDialog.WantToDestroy == true)
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
}
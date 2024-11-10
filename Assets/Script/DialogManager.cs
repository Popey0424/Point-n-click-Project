using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TextDialog
{
    public string[] TextString;
    public int InteractionID;
    public Button ContinueText;
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
    [SerializeField] private string currentline;
    [SerializeField] private int textId;
    [SerializeField] public int maxPoints;

    [Header("Debug")]
    [SerializeField] private bool isTyping;
    [SerializeField] private bool skipTyping;

    private int points;


    private int currentInteractionId;

    private void Start()
    {
        continueButton.SetActive(false);
    }

    public void StartDialogue(int interactionId)
    {
        HUDdialog.SetActive(true);
        TextDialog dialog = null;


        currentInteractionId = interactionId;

        for (int i = 0; i < dialogList.Count; i++)
        {
            if (dialogList[i].InteractionID == interactionId)
            {
                dialog = dialogList[i];
                break;
            }
        }

        textId = 0;
        StartCoroutine(TypeLine(dialog));
    }

    private IEnumerator TypeLine(TextDialog dialog)
    {
        isTyping = true;
        currentline = dialog.TextString[textId];
        textDialog.text = "";

        foreach (char letter in currentline.ToCharArray())
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        continueButton.SetActive(true);
    }

    public void OnClickContinueDialog()
    {
        if (isTyping)
        {
            return;
        }


        TextDialog dialog = null;
        for (int i = 0; i < dialogList.Count; i++)
        {
            if (dialogList[i].InteractionID == currentInteractionId)
            {
                dialog = dialogList[i];
                break;
            }
        }

        if (dialog == null)
        {
            return;
        }
        int pointsToAdd = GetPointsForLine(dialog.TextString[textId]);
        points += pointsToAdd;

        if (pointsToAdd >= maxPoints)
        {
            //mort
        }
        textId++;
        if (textId < dialog.TextString.Length)
        {
            StartCoroutine(TypeLine(dialog));
        }
        else
        {
            EndDialogue();
        }    
    }
    private int GetPointsForLine(string line)
    {
        return line.Contains("important") ? 10 : 1;
    }

    private void EndDialogue()
    {
        continueButton.SetActive(false);
        textDialog.text = "";
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TextDialogIntro
{
    public enum CharacterType { Buddy, Inconnu, Mother, Sister, Robbie, Scott }

    [System.Serializable]
    public class DialogLineIntro
    {
        public CharacterType characterType;
        public string text;
    }

    public List<DialogLineIntro> dialogLines;
    public int InteractionID;
    public Button ContinueText;
}

public class DialogManagerIntro : MonoBehaviour
{
    [Header("List")]
    [SerializeField] private List<TextDialogIntro> dialogListIntro = new List<TextDialogIntro>();

    [Header("Dialogue Settings")]
    [SerializeField] private GameObject HUDdialog;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private string GotoScene;

    private TextDialogIntro currentDialog;
    private int textId;
    public int TextInteraction;

    [Header("Image Fade")]
    [SerializeField] private Image ImageFade;

    private void Start()
    {
        continueButton.SetActive(false);
        StartDialogue(TextInteraction);
    }

    public void StartDialogue(int interactionId)
    {
        
        HUDdialog.SetActive(true);
        textDialog.gameObject.SetActive(true);
        continueButton.SetActive(false);



        currentDialog = dialogListIntro.Find(d => d.InteractionID == interactionId);

        if (currentDialog != null)
        {
            textId = 0;
            StartCoroutine(TypeLine());
        }
    }


    private IEnumerator TypeLine()
    {
        if (currentDialog == null || textId >= currentDialog.dialogLines.Count) yield break;

        TextDialogIntro.DialogLineIntro line = currentDialog.dialogLines[textId];


        characterNameText.text = line.characterType.ToString();
        textDialog.text = "";

        foreach (char letter in line.text)
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }
    public void OnClickContinueDialogIntro()
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAh");
        //if (currentDialog == null) return;

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
        

        if (dialogListIntro.Contains(currentDialog))
        {
            dialogListIntro.Remove(currentDialog);
        }
        ImageFade.gameObject.SetActive(true);
        ImageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);


        currentDialog = null;
    }

    private void FadeComplete()
    {
        SceneManager.LoadScene(GotoScene);
    }
}



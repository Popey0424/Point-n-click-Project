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
    [SerializeField] private GameObject continueButton;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private string currentline;
    [SerializeField] private int textId;

    [Header("Debug")]
    [SerializeField] private bool isTyping;
    [SerializeField] private bool skipTyping;


    private void Start()
    {

        continueButton.SetActive(false);
    }
    private void Update()
    {
       
    }



    //public void StartDialogue(string[] dialogueLines)
    //{
    //    StartCoroutine(DisplayLines(dialogueLines));
    //}

    //IEnumerator DisplayLines(string[] lines)
    //{
    //    foreach (string line in lines)
    //    {
    //        yield return StartCoroutine(TypeLine(line));  
           
    
    //    }
    //}

    //IEnumerator TypeLine(string line)
    //{
    //    yield return new WaitForSeconds(typingSpeed);
    //}


    

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [SerializeField] private GameObject continueButton;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private string currentline;

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


    public void StartDialogue(string[] dialogueLines)
    {
        StartCoroutine(DisplayLines(dialogueLines));
    }

    IEnumerator DisplayLines(string[] lines)
    {
        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            continueButton.SetActive(true);  
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); 
            continueButton.SetActive(false);
        }
    }

    IEnumerator TypeLine(string line)
    {
        yield return new WaitForSeconds(typingSpeed);
    }

}

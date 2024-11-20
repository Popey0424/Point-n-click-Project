using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DebugDialog
{
    public string[] textString;
    public string charactername;
    public int debugInteractionID;
    public Button ContinueDebugText;
}


public class Chapter1Start : MonoBehaviour
{
    private CameraController cameraController;
    private PlayerMovement playerMovement;
    [SerializeField] private List<DebugDialog> debugDialogs = new List<DebugDialog>();
    private DebugDialog currentDebugDialog;


    [Header("Start Interactions")]
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private TextMeshProUGUI textCharacterName;
    [SerializeField] private GameObject HUDdialog;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private Image RaycastImage;
    [SerializeField] private GameObject continueButton;


    [Header("Debug")]
    [SerializeField] private bool IsMouseIn = false;
    private int ID;


    #region firstRoom

    public GameObject RobbieWithoutBuddy;
    public GameObject RobbieWithBuddy;




    #endregion

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void StartDebugDialogue(int debuginteractionID)
    {
        HUDdialog.SetActive(true);
        textDialog.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
        if (playerMovement != null)
        {
            playerMovement.StopMovement(false);
        }

        currentDebugDialog = debugDialogs.Find(e => e.debugInteractionID == debuginteractionID);
        if (currentDebugDialog != null)
        {
            ID = 0;
            StartCoroutine(TypeLine(currentDebugDialog));
        }
    }


    private IEnumerator TypeLine(DebugDialog dialog)
    {
        textDialog.text = "";

        foreach (char letter in dialog.textString[ID])
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    public void OnClickContinueDialog()
    {
        if (currentDebugDialog == null) return;


        if (ID < currentDebugDialog.textString.Length - 1)
        {
            ID++;
            continueButton.SetActive(false);
            StartCoroutine(TypeLine(currentDebugDialog));
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


        //if (debugDialogs.Contains(currentDebugDialog))
        //{
        //    debugDialogs.Remove(currentDebugDialog);
        //}




        currentDebugDialog = null;
    }


    //private void OnMouseDown()
    //{
    //    cameraController.isBudddyHere = true;
    //}

    public void SwitchRobbieSkin()
    {
        Debug.Log("Changement de skin");
        RobbieWithoutBuddy.gameObject.SetActive(false);
        RobbieWithBuddy.gameObject.SetActive(true);
     
    }

}

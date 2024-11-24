using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MindDialogManager
{
    public string[] textString;
    public string charactername;
    public int MindDialogID;
    public Button ContinueMindText;

}


public class S_Chapter : MonoBehaviour
{
    private Inventory playerInventory;
    private PlayerMovement1 playerMovement1;
    [SerializeField] private List<MindDialogManager> mindDialog = new List<MindDialogManager>();
    [SerializeField] private string requiredBuddy;
    [SerializeField] private GameObject DialogHUD;
    [SerializeField] private GameObject continueButton;
    private MindDialogManager currentMindDialog;
    public bool trouverBuddy = false;

    [Header("Reference")]
    [SerializeField] private TextMeshProUGUI textDialog;
    [SerializeField] private TextMeshProUGUI textCharacterName;
    [SerializeField] private float typingSpeed = 0.05f;

    [Header("Trigger")]
    public bool CompleteIterraction = false;


    private int ID;

    private void Start()
    {
        playerMovement1 = FindObjectOfType<PlayerMovement1>();
    }

    private void Update()
    {
        {
            if (playerInventory == null)
            {
                GameObject playerObj = GameObject.FindWithTag("Player");
                if (playerObj != null)
                {
                    playerInventory = playerObj.GetComponent<Inventory>();
                }
            }
            if (playerInventory != null && playerInventory.HasItem(requiredBuddy))
            {
                TriggerEventRoom();
                trouverBuddy = true;
                //playerMovement.SwitchRobbieSkin();
                Debug.Log("Buddy trouvé fleche debloquer");

            }

        }
    }

    public void StartMindDialogue(int MindInteractionID)
    {
        continueButton.SetActive(false);
        DialogHUD.SetActive(true);
        if(playerMovement1 != null)
        {
            playerMovement1.StopMovement(false);
        }
        currentMindDialog = mindDialog.Find(e => e.MindDialogID == MindInteractionID);
        if(currentMindDialog != null)
        {
            ID = 0;
            StartCoroutine(TypeLine(currentMindDialog));
        }

    }

    private IEnumerator TypeLine(MindDialogManager minddialog)
    {
        textDialog.text = "";

        foreach (char letter in minddialog.textString[ID])
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }


    public void OnClickContinueDialog()
    {
        if(currentMindDialog == null)
        {
            return;
        }

        if(ID < currentMindDialog.textString.Length - 1)
        {
            ID++;
            continueButton.SetActive(false);
            StartCoroutine(TypeLine(currentMindDialog));
        }
        else
        {
            EndDialog();
        }
    }
    private void EndDialog()
    {
        DialogHUD.SetActive(false);
        continueButton.SetActive(false);
        textDialog.text = "";

        if(playerMovement1 != null)
        {
            playerMovement1.StopMovement(true);
        }

        currentMindDialog = null;
    }


    public void TriggerEventRoom()
    {
        Debug.Log("Trigger Zone 0");
        if (playerInventory != null && playerInventory.HasItem(requiredBuddy))
        {
            CompleteIterraction = true;
            
        }
    }
}

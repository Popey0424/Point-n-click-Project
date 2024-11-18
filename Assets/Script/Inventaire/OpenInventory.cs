using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private bool isInventoryOpen= false;

    [SerializeField] private Button LeaveInventory;

   


    private void Start()
    {
        inventory.SetActive(false);
    }
    public void OnClickInventory()
    {
        inventory.SetActive(true);
        LeaveInventory.gameObject.SetActive(true);
        isInventoryOpen = true;
    }
    public void OnClickLeaveInventory()
    {
        if(isInventoryOpen == false)
        {
            inventory.SetActive(false);
            LeaveInventory.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Marche pas l'inventaire nest pas ouvert");
        }
    }
   
}

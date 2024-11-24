using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private bool isInventoryOpen= false;

    [SerializeField] private Button LeaveInventory;
    public PlayerMovement1 PlayerMovement1;

   


    private void Start()
    {
        inventory.SetActive(false);
        PlayerMovement1 = FindObjectOfType<PlayerMovement1>();
    }
    public void OnClickInventory()
    {
        inventory.SetActive(true);
        LeaveInventory.gameObject.SetActive(true);
        isInventoryOpen = true;

        if(PlayerMovement1 != null)
        {
            PlayerMovement1.StopMovement(false);
        }
    }
    public void OnClickLeaveInventory()
    {
        if(isInventoryOpen == false)
        {
            Debug.Log("Ouvert");
        }
        else
        {
            if(PlayerMovement1 != null)
            {
                PlayerMovement1.StopMovement(true);
            }
            inventory.SetActive(false);
            LeaveInventory.gameObject.SetActive(false);
        }
    }
   
}

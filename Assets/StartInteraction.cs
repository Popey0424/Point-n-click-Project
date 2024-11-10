using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInteraction : MonoBehaviour
{
    public DialogManager DialogManager;
    [SerializeField] private int interactionID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.StartDialogue(interactionID);
        }
    }
}

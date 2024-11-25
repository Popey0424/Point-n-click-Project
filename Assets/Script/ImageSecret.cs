using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSecret : MonoBehaviour
{
    [SerializeField] public PlayerMovement1 playerMovement1;
    [SerializeField] private GameObject Image;
    [SerializeField] private GameObject LeaveButton;
    [SerializeField] private GameObject RaycastImage;
    // Start is called before the first frame update


    private void OnMouseEnter()
    {
        Debug.Log("Mouse In");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse Out");
    }

    private void OnMouseDown()
    {
        OpenImage();
    }


    private void OpenImage()
    {
        if (playerMovement1 != null)
        {
            playerMovement1.StopMovement(false);
        }
        Image.gameObject.SetActive(true);
        LeaveButton.gameObject.SetActive(true);
        RaycastImage.gameObject.SetActive(true);
    }

    void Start()
    {
        LeaveButton.gameObject.SetActive(false);
        Image.gameObject.SetActive (false);
        RaycastImage.gameObject.SetActive(false);
    }

    public void CloseImage()
    {
        LeaveButton.gameObject.SetActive(false);
        Image.gameObject.SetActive(false);
        RaycastImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

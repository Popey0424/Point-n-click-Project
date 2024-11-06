using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [Header("Settings Camera")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private int currentRoom = 0;
    [SerializeField] private Image imageFade;

    [Header("Arrows Buttons")]
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private bool isLeftArrowExist;
    [SerializeField] private bool isRightArrowExist;

    [Header("Debug")]
    [SerializeField] private bool isMoving = false;

    //private Objects
    private Vector3 targetPosition;

    private void Start()
    {
        imageFade.gameObject.SetActive(false);
        UpdateTargetPosition();
    }

    void Update()
    {
        #region check Possibilité Boutons 
        leftArrow.gameObject.SetActive(isLeftArrowExist);
        rightArrow.gameObject.SetActive(isRightArrowExist);
        #endregion

        if (isMoving)
        {
            MoveCamera();
        }
    }

    private void UpdateTargetPosition()
    {
        targetPosition = new Vector3(backgrounds[currentRoom].position.x, transform.position.y, transform.position.z);
    }


    public void OnClickRightArrow()
    {
       

        MoveToNextBackground(1);
    }

    public void OnClickLeftArrow()
    {
    

        MoveToNextBackground(-1);
    }



    private void MoveCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f) 
        {
            transform.position = targetPosition;
            isMoving = false;
            StartFadeOut();
        }
    }

    private void MoveToNextBackground(int direction)
    {
    
        currentRoom += direction;
        currentRoom = Mathf.Clamp(currentRoom, 0, backgrounds.Length - 1); 

        UpdateTargetPosition();
        StartFadeIn();
    }


    #region Fade


    private void StartFadeIn()
    {
        imageFade.gameObject.SetActive(true);
        isLeftArrowExist = true;
        isRightArrowExist = true;
        imageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);

    }
    private void FadeComplete()
    {

        isMoving = true;
       
    }

    private void StartFadeOut()
    {
        imageFade.DOFade(0, 1.5f).OnComplete(ResetFade);
    }

    private void ResetFade()
    {
        imageFade.gameObject.SetActive(false);
        isLeftArrowExist = currentRoom > 0;
        isRightArrowExist = currentRoom < backgrounds.Length - 1;
    }


  

    #endregion
}



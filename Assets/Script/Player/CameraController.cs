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
    [SerializeField] public bool isBudddyHere = false;

    public Chapter1Start chapter1Start;
    public Inventory playerInventory;
    [SerializeField] private string requiredBuddyItem;

    // Player reference
    private GameObject player;
    [SerializeField] private float playerMoveSpeed = 5f; 

    // Private objects
    private Vector3 targetPosition;

    private void Start()
    {
        imageFade.gameObject.SetActive(false);
        UpdateTargetPosition();

        
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        #region Check Possibility Buttons
        leftArrow.gameObject.SetActive(isLeftArrowExist);
        rightArrow.gameObject.SetActive(isRightArrowExist);
        #endregion

        if (isMoving)
        {
            MoveCamera();
        }
        if (playerInventory == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                playerInventory = playerObj.GetComponent<Inventory>();
            }
        }

        if (playerInventory != null && playerInventory.HasItem(requiredBuddyItem))
        {
            chapter1Start.SwitchRobbieSkin();
            Debug.Log("Buddy trouvé fleche debloquer");
           
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
        
        if (playerInventory != null && playerInventory.HasItem(requiredBuddyItem))
        {
           
            Debug.Log("Buddy trouvé");
            MoveToNextBackground(-1);
        }
        else
        {
            Debug.Log("Besoin de buddy");
            chapter1Start.StartDebugDialogue(1);
        }
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
        MovePlayerToEdge(direction); // Move the player
        StartFadeIn();
    }

    private void MovePlayerToEdge(int direction)
    {
        if (player == null) return;

        // Determine the target position for the player
        float targetX = direction > 0 ? backgrounds[currentRoom].position.x - 1f : backgrounds[currentRoom].position.x + 1f; // Adjust offsets as needed
        Vector3 playerTargetPosition = new Vector3(targetX, player.transform.position.y, player.transform.position.z);

        // Move the player smoothly
        player.transform.DOMove(playerTargetPosition, playerMoveSpeed).SetEase(Ease.Linear);
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
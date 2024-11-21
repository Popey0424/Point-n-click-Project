using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [Header("Settings Camera")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform[] backgrounds; // Les différentes salles
    [SerializeField] private Transform[] exitPoints; // Les points de sortie pour chaque salle
    [SerializeField] private int currentRoom = 0;
    [SerializeField] private Image imageFade;

    [Header("Arrows Buttons")]
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private bool isLeftArrowExist;
    [SerializeField] private bool isRightArrowExist;

    [Header("Debug")]
    [SerializeField] private bool isMoving = false;
    [SerializeField] public bool isBuddyHere = false;

    public Chapter1Start chapter1Start;
    public Inventory playerInventory;
    [SerializeField] private string requiredBuddyItem;

    // Player reference
    [SerializeField] private GameObject player;  // Rendre le joueur modifiable dans l'éditeur
    [SerializeField] private float playerMoveSpeed = 5f;

    // Private objects
    private Vector3 targetPosition;
    private Transform exitPoint; // Point de sortie pour chaque salle

    private void Start()
    {
        imageFade.gameObject.SetActive(false);
        UpdateTargetPosition();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");
            }
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

        if (playerInventory != null && playerInventory.HasItem(requiredBuddyItem))
        {
            chapter1Start.hasBuddy = true;
            chapter1Start.SwitchRobbieSkin();
            Debug.Log("Buddy trouvé, flèche débloquée");
        }
    }

    private void UpdateTargetPosition()
    {
        targetPosition = new Vector3(backgrounds[currentRoom].position.x, transform.position.y, transform.position.z);
    }

    public void OnClickRightArrow()
    {
        if (playerInventory != null && playerInventory.HasItem(requiredBuddyItem))
        {
            MoveToNextBackground(1);
            Debug.Log("Buddy trouvé");
        }
        else
        {
            Debug.Log("Besoin de buddy");
            chapter1Start.StartDebugDialogue(1);
        }
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

        // Récupérer le point de sortie spécifique à la salle
        exitPoint = GetExitPoint(currentRoom);

        // Déplacer le joueur vers le point de sortie
        MovePlayerToExit(exitPoint);

        StartFadeIn();
    }

    private Transform GetExitPoint(int roomIndex)
    {
        // Retourne le point de sortie pour la salle en cours en utilisant l'index
        if (roomIndex >= 0 && roomIndex < exitPoints.Length)
        {
            return exitPoints[roomIndex];  // Retourne le point de sortie spécifique à la salle
        }
        return null; // Si l'index est invalide, retourner null
    }

    private void MovePlayerToExit(Transform exit)
    {
        if (player == null || exit == null) return;

        // Déplace le joueur vers le point de sortie (par exemple la porte ou l'escalier)
        player.transform.DOMove(exit.position, playerMoveSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveCameraToNewRoom();
        });
    }

    private void MoveCameraToNewRoom()
    {
        // Déplace la caméra vers la nouvelle position
        targetPosition = new Vector3(backgrounds[currentRoom].position.x, exitPoint.position.y, transform.position.z);
        isMoving = true;
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
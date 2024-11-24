using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField] private GameObject idleState;
    [SerializeField] private GameObject walkingState;
    public LayerMask GroundLayer;
    public float speed = 5f;
    public float stopDistance = 0.1f;

    private Vector2 targetPosition;
    private bool isMoving = false;
    private bool canMove = true;

    [SerializeField] private Transform entryPoint;
    private bool isEnteringScene = true;
    [SerializeField] private Transform[] directionalPoints;

    public S_Chapter s_Chapter;

    [SerializeField] private GameObject specificIdlePrefab;
    [SerializeField] private GameObject specificWalkingPrefab;
    [SerializeField] private string specificSceneName;

    private bool spritesReplaced = false; 

    void Start()
    {
        if (entryPoint != null)
        {
            targetPosition = entryPoint.position;
            isMoving = true;
            canMove = false;
            SetWalkingState();
        }
        else
        {
            targetPosition = transform.position;
            SetIdleState();
        }
    }

    void Update()
    {
       
        if (!spritesReplaced && SceneManager.GetActiveScene().name == specificSceneName && s_Chapter.trouverBuddy == true)
        {
            ReplacePrefabs();
            spritesReplaced = true; 
        }

        if (isEnteringScene)
        {
            MovePlayer();
            if (!isMoving)
            {
                isEnteringScene = false;
                canMove = true;
            }
            return;
        }

        HandleInput();
        MovePlayer();
    }

    public void StopMovement(bool enable)
    {
        canMove = enable;
        if (!enable)
        {
            isMoving = false;
            SetIdleState();
        }
    }

    private void SetIdleState()
    {
        idleState.SetActive(true);
        walkingState.SetActive(false);
    }

    private void HandleInput()
    {
        if (canMove && Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, GroundLayer);

            if (hit.collider != null)
            {
                targetPosition = hit.point;
                isMoving = true;
                FlipSprite(targetPosition.x);
                SetWalkingState();
            }
        }
    }

    public void MoveToDirection(int directionIndex)
    {
        if (directionIndex >= 0 && directionIndex < directionalPoints.Length)
        {
            targetPosition = directionalPoints[directionIndex].position;
            isMoving = true;
            FlipSprite(targetPosition.x);
            SetWalkingState();
        }
    }

    void MovePlayer()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) <= stopDistance)
            {
                isMoving = false;
                SetIdleState();
            }
        }
    }

    private void SetWalkingState()
    {
        idleState.SetActive(false);
        walkingState.SetActive(true);
    }

    private void FlipSprite(float targetXPosition)
    {
        if (targetXPosition < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void ReplacePrefabs()
    {
        Debug.Log("Switching sprites...");
        if (specificIdlePrefab != null)
        {
            Destroy(idleState);
            idleState = Instantiate(specificIdlePrefab, transform);
            idleState.SetActive(true);
        }

        if (specificWalkingPrefab != null)
        {
            Destroy(walkingState);
            walkingState = Instantiate(specificWalkingPrefab, transform);
            walkingState.SetActive(true);
        }
    }
}
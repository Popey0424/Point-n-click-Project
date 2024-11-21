using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Movement")]
    public LayerMask floorLayer;
    [SerializeField] private float moveSpeed = 5;
    private Vector2 targetPosition;
    [Header("Debug")]
    [SerializeField] private bool isMoving = false;
    private bool canMove = true;

    // R�f�rences aux Prefabs
    public GameObject RobbieWithoutBuddyIdle;
    public GameObject RobbieWithoutBuddyWalk;
    public GameObject RobbieWithBuddyIdle;
    public GameObject RobbieWithBuddyWalk;

    private GameObject currentRobbieIdle; // Le prefab actuellement actif pour l'animation Idle
    private GameObject currentRobbieWalk; // Le prefab actuellement actif pour l'animation Walk

    private void Start()
    {
        // Initialisation: Robbie sans nounours en idle
        currentRobbieIdle = RobbieWithoutBuddyIdle;
        currentRobbieWalk = RobbieWithoutBuddyWalk;

        currentRobbieIdle.SetActive(true); // Activer l'�tat Idle sans nounours
        currentRobbieWalk.SetActive(false); // D�sactiver l'�tat Walk sans nounours
    }

    private void Update()
    {
        if (canMove && Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, floorLayer);

            if (hit.collider != null)
            {
                targetPosition = hit.point;
                isMoving = true;
                FlipSprite(targetPosition.x);
                StartWalking(); // D�marre l'animation de marche
            }
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            StopWalking(); // Arr�te l'animation de marche et passe en idle
        }
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

    public void StopMovement(bool enable)
    {
        canMove = enable;
        if (!enable)
        {
            isMoving = false;
            StopWalking(); // Arr�ter la marche si le mouvement est arr�t�
        }
    }

    private void StartWalking()
    {
        // D�sactive l'animation Idle et active l'animation Walk du bon prefab
        currentRobbieIdle.SetActive(false);
        currentRobbieWalk.SetActive(true);
    }

    private void StopWalking()
    {
        // D�sactive l'animation Walk et active l'animation Idle du bon prefab
        currentRobbieWalk.SetActive(false);
        currentRobbieIdle.SetActive(true);
    }

    public void SwitchRobbieSkin()
    {
        Debug.Log("Changement de skin");

        // D�sactive les animations de marche et idle avant de changer de skin
        currentRobbieIdle.SetActive(false);
        currentRobbieWalk.SetActive(false);

        // Si Robbie a r�cup�r� son nounours
        if (currentRobbieIdle == RobbieWithoutBuddyIdle)
        {
            // D�sactive les prefab sans nounours
            RobbieWithoutBuddyIdle.SetActive(false);
            RobbieWithoutBuddyWalk.SetActive(false);

            // Active les prefab avec nounours
            RobbieWithBuddyIdle.SetActive(true);
            RobbieWithBuddyWalk.SetActive(false);

            // Met � jour les objets actifs
            currentRobbieIdle = RobbieWithBuddyIdle;
            currentRobbieWalk = RobbieWithBuddyWalk;
        }
        else
        {
            // D�sactive les prefab avec nounours
            RobbieWithBuddyIdle.SetActive(false);
            RobbieWithBuddyWalk.SetActive(false);

            // Active les prefab sans nounours
            RobbieWithoutBuddyIdle.SetActive(true);
            RobbieWithoutBuddyWalk.SetActive(false);

            // Met � jour les objets actifs
            currentRobbieIdle = RobbieWithoutBuddyIdle;
            currentRobbieWalk = RobbieWithoutBuddyWalk;
        }

        // Si le joueur a r�cup�r� son nounours, on commence directement l'animation de marche avec nounours
        if (currentRobbieIdle == RobbieWithBuddyIdle)
        {
            StartWalking(); // Lance l'animation de marche avec nounours si n�cessaire
        }
        else
        {
            StopWalking(); // Sinon, passe en idle
        }
    }
}
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
    public GameObject PersoWithoutBuddyIdle;
    public GameObject PersoWithoutBuddyWalk;
    public GameObject PersoWithBuddyIdle;
    public GameObject PersoWithBuddyWalk;

    private GameObject currentRobbieIdle; // Le prefab actuellement actif pour l'animation Idle
    private GameObject currentRobbieWalk; // Le prefab actuellement actif pour l'animation Walk

    [SerializeField] private bool isGonnaChangeSkin = false;

    private void Start()
    {
        // Initialisation: Robbie sans nounours en idle
        currentRobbieIdle = PersoWithoutBuddyIdle;
        currentRobbieWalk = PersoWithoutBuddyWalk;

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
        if(isGonnaChangeSkin == true)
        {
            Debug.Log("Changement de skin");

            // D�sactive les animations de marche et idle avant de changer de skin
            currentRobbieIdle.SetActive(false);
            currentRobbieWalk.SetActive(false);

            // Si Robbie a r�cup�r� son nounours
            if (currentRobbieIdle == PersoWithoutBuddyIdle)
            {
                // D�sactive les prefab sans nounours
                PersoWithoutBuddyIdle.SetActive(false);
                PersoWithoutBuddyWalk.SetActive(false);

                // Active les prefab avec nounours
                PersoWithBuddyIdle.SetActive(true);
                PersoWithBuddyWalk.SetActive(false);

                // Met � jour les objets actifs
                currentRobbieIdle = PersoWithBuddyIdle;
                currentRobbieWalk = PersoWithBuddyWalk;
            }
            else
            {
                // D�sactive les prefab avec nounours
                PersoWithBuddyIdle.SetActive(false);
                PersoWithBuddyWalk.SetActive(false);

                // Active les prefab sans nounours
                PersoWithoutBuddyIdle.SetActive(true);
                PersoWithoutBuddyWalk.SetActive(false);

                // Met � jour les objets actifs
                currentRobbieIdle = PersoWithoutBuddyIdle;
                currentRobbieWalk = PersoWithoutBuddyWalk;
            }

            // Si le joueur a r�cup�r� son nounours, on commence directement l'animation de marche avec nounours
            if (currentRobbieIdle == PersoWithBuddyIdle)
            {
                StartWalking(); // Lance l'animation de marche avec nounours si n�cessaire
            }
            else
            {
                StopWalking(); // Sinon, passe en idle
            }
        }
        
    }
}
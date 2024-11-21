using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class CameraController : MonoBehaviour
{
    [Header("Settings Camera")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private List<Room> rooms; 
    [SerializeField] private int currentRoomIndex = 0;
    [SerializeField] private Image imageFade;

    [Header("Arrows Buttons")]
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private Button upArrow;
    [SerializeField] private Button downArrow;
    [SerializeField] private bool isLeftArrowExist;
    [SerializeField] private bool isRightArrowExist;

    [Header("Player Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private float playerMoveSpeed = 5f;
    public Chapter1Start chapter1;
    private bool isMoving = false;

    private void Start()
    {
        imageFade.gameObject.SetActive(false);

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("pas de player");
            }
        }

        PositionPlayerAtEntry(currentRoomIndex);
        UpdateArrowPositions();
    }

    void Update()
    {
        UpdateArrowVisibility();
    }

    private void PositionPlayerAtEntry(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < rooms.Count)
        {
            Transform entryPoint = rooms[roomIndex].entryPoint;
            if (entryPoint != null && player != null)
            {
                player.transform.position = entryPoint.position;
            }
        }
    }

    public void OnClickRightArrow()
    {
        if (currentRoomIndex == 0)
        {
            Debug.Log("OULA");
            chapter1.TriggerEventRoom0();
            if (chapter1.Complete1 == true)
            {
                Debug.Log("Normaelemnt cest bon ");
                MoveToRoom(currentRoomIndex + 1);
            }
            else
            {
                // MEttre text Erreur
            }
           
        }
        else if (currentRoomIndex < rooms.Count - 1)
        {
            MoveToRoom(currentRoomIndex + 1);
        }
        
    }

    public void OnClickLeftArrow()
    {
        if (currentRoomIndex > 0)
        {
            MoveToRoom(currentRoomIndex - 1);
        }
    }


    private void MoveToRoom(int newRoomIndex)
    {
        if (newRoomIndex < 0 || newRoomIndex >= rooms.Count || isMoving)
            return;

        Room currentRoom = rooms[currentRoomIndex];
        Room nextRoom = rooms[newRoomIndex];

   
        if (currentRoom.exitPoint != null)
        {
            MovePlayerToPoint(currentRoom.exitPoint, () =>
            {
               
                currentRoomIndex = newRoomIndex;

                StartFadeIn();
                MoveCameraToRoom(nextRoom);

              
                PositionPlayerAtEntry(currentRoomIndex);
                //UpdateArrowPositions();
            });
        }
    }

    private void MovePlayerToPoint(Transform point, TweenCallback onComplete)
    {
        if (player == null || point == null)
            return;

        player.transform.DOMove(point.position, playerMoveSpeed).SetEase(Ease.Linear).OnComplete(onComplete);
    }

    private void MoveCameraToRoom(Room room)
    {
        if (room != null)
        {
            isMoving = true;
            Vector3 targetPosition = new Vector3(room.cameraPosition.position.x, room.cameraPosition.position.y, transform.position.z);

            transform.DOMove(targetPosition, moveSpeed).OnComplete(() =>
            {
                isMoving = false;
                StartFadeOut();
                UpdateArrowVisibility();
            });
        }
    }

    private void UpdateArrowPositions()
    {
        Room currentRoom = rooms[currentRoomIndex];
        if (currentRoom.leftArrowPosition != null)
        {
            leftArrow.transform.position = currentRoom.leftArrowPosition.position;
        }
        if (currentRoom.rightArrowPosition != null)
        {
            rightArrow.transform.position = currentRoom.rightArrowPosition.position;
        }
            
    }
    private void UpdateArrowVisibility()
    {
        Room currentRoom = rooms[currentRoomIndex];

        // Activer/Désactiver les flèches
        leftArrow.gameObject.SetActive(currentRoom.leftArrowPosition != null);
        rightArrow.gameObject.SetActive(currentRoom.rightArrowPosition != null);
    }


        [System.Serializable]
    public class Room
    {
        public Transform cameraPosition; 
        public Transform entryPoint;     
        public Transform exitPoint;

        public Transform leftArrowPosition;
        public Transform rightArrowPosition;

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
        UpdateArrowPositions();
    }

    private void StartFadeOut()
    {
        imageFade.DOFade(0, 1.5f).OnComplete(ResetFade);
    }

    private void ResetFade()
    {
        imageFade.gameObject.SetActive(false);
    }

    #endregion
}

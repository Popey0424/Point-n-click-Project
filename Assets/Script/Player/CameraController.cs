using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [Header("Settings Camera")]
     private float moveSpeed = 150f;
    [SerializeField] private List<Room> rooms; 
    [SerializeField] private int currentRoomIndex = 0;
    [SerializeField] private Image imageFade;

    [Header("Player Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private float playerMoveSpeed = 5f;

    private bool isMoving = false;

    private void Start()
    {
        imageFade.gameObject.SetActive(false);

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("JOuer pas trouver");
            }
        }

        PositionPlayerAtEntry(currentRoomIndex);
        UpdateRoomDirections();
    }

    private void Update()
    {
        Debug.Log(moveSpeed);
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

    private void UpdateRoomDirections()
    {
        Room currentRoom = rooms[currentRoomIndex];


        foreach (var direction in currentRoom.directions)
        {
            direction.arrow.gameObject.SetActive(false);
        }

        foreach (var direction in currentRoom.directions)
        {
            direction.arrow.gameObject.SetActive(true);
        }
    }

    public void HandleDirection(int directionIndex)
    {
        if (isMoving) return;

        Room currentRoom = rooms[currentRoomIndex];

        if (directionIndex < 0 || directionIndex >= currentRoom.directions.Count)
        {
            Debug.LogError("Direction index out of bounds!");
            return;
        }

        RoomDirection direction = currentRoom.directions[directionIndex];

     
        if (direction.transitionPoint != null)
        {
            MovePlayerToPoint(direction.transitionPoint, () =>
            {

                MoveToRoom(direction.targetRoomIndex);
            });
        }
        else
        {

            MoveToRoom(direction.targetRoomIndex);
        }
    }

    private void MovePlayerToPoint(Transform point, TweenCallback onComplete)
    {
        if (player == null || point == null)
            return;

        player.transform.DOMove(point.position, playerMoveSpeed).SetEase(Ease.Linear).OnComplete(onComplete);
    }

    private void MoveToRoom(int newRoomIndex)
    {
        if (newRoomIndex < 0 || newRoomIndex >= rooms.Count || isMoving)
            return;

        Room nextRoom = rooms[newRoomIndex];


        MoveCameraToRoom(nextRoom);


        PositionPlayerAtEntry(newRoomIndex);


        currentRoomIndex = newRoomIndex;
        UpdateRoomDirections();
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
            });
        }
    }

    [System.Serializable]
    public class Room
    {
        public Transform cameraPosition;   
        public Transform entryPoint;       
        public List<RoomDirection> directions; 
    }

    [System.Serializable]
    public class RoomDirection
    {
        public Button arrow;              
        public int targetRoomIndex;        
        public Transform transitionPoint;  
    }

    #region Fade

    private void StartFadeIn()
    {
        imageFade.gameObject.SetActive(true);
        
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
    }

    #endregion
}

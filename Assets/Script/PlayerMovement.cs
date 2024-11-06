using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Movement")]
    public LayerMask floorLayer;
    [SerializeField] private float moveSpeed = 5;

   

    //private objects
    private Vector2 targetpostion;


    [Header("Debug")]
    [SerializeField] private bool isMoving = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, floorLayer);

            
            if (hit.collider != null)
            {
                
                targetpostion = hit.point;
                isMoving = true;
            }
        }
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetpostion, moveSpeed * Time.deltaTime);

        
        if (Vector2.Distance(transform.position, targetpostion) < 0.1f)
        {
            isMoving = false;
        }
    }
                  
}

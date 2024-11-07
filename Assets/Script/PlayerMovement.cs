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
    private Vector2 targetposition;


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
                
                targetposition = hit.point;
                isMoving = true;
                FlipSprite(targetposition.x);
            }
        }
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetposition, moveSpeed * Time.deltaTime);

        
        if (Vector2.Distance(transform.position, targetposition) < 0.1f)
        {
            isMoving = false;
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

}

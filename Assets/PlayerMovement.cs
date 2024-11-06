using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Movement")]
    [SerializeField] private float moveSpeed = 5;


    //private objects
    private Vector3 targetpostion;

    [Header("Debug")]
    [SerializeField] private bool isMoving = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clique Detecter");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
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
        transform.position = Vector3.MoveTowards(transform.position, targetpostion, moveSpeed * Time.deltaTime);
        if (transform.position == targetpostion)
        {
            isMoving = false; 
        }
    }
                  
}

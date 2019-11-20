using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hit;
    [SerializeField] Vector3 cameraOffset = new Vector3(0f, 7f, 7f);
    bool canMove = true;
    Animator animator;
    float speed;
    bool attacking = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        speed = agent.velocity.magnitude;
        Camera.main.transform.position = transform.position + cameraOffset;
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0) && canMove)
        {
            MoveToClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Stop();
            Attack();
        }
    }

    private void Stop()
    {
        agent.destination = transform.position;
    }

    private void MoveToClick()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            agent.destination = hit.point;
        }
    }

    private void Attack()
    {
        print("attacking");
        animator.SetTrigger("attack");
        canMove = false;
        Invoke("AllowToMove", .5f);
    }

    void AllowToMove()
    {
        canMove = true;
    }

    private bool JustFinishedMoving()
    {
        return true;
    }
}

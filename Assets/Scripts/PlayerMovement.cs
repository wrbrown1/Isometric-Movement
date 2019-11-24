using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Vector3 cameraOffset = new Vector3(0f, 7f, 7f);
    Animator animator;
    float speed;
    public LayerMask movementMask;
    public Interactable focus;
    Transform target;
    bool tracking = false;
    bool inCombat = false;
    Interactable interactable;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetSpeed();
        HandleMovement();
        AnimateMovement();
        CheckIfEngagingTarget();
    }

    private void CheckIfEngagingTarget()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    private void FaceTarget() //Study this later
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void GetSpeed()
    {
        speed = agent.velocity.magnitude;
    }

    private void AnimateMovement()
    {
        speed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speed", speed, .1f, Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0) && !inCombat)
        {
            StopFocusing();
            MoveToClick();
        }
        else if (Input.GetMouseButtonDown(1) && !inCombat)
        {
            Interact();
        }
    }

    private void MoveToClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, movementMask))
        {
            agent.destination = hit.point;
            StopTrackingTarget();
        }
    }

    private void Interact()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            interactable = hit.collider.GetComponent<Interactable>();
            if(interactable != null)
            {
                Focus(interactable);
            }
        }
    }

    private void Focus(Interactable newFocus)
    {
        focus = newFocus;
        TrackTarget(newFocus);
    }

    private void StopFocusing()
    {
        focus = null;
        StopTrackingTarget();
    }

    //private void Attack()
    //{
    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
    //    {
    //        transform.rotation = Quaternion.LookRotation(hit.point);
    //        Stop();
    //        canMove = false;
    //        canAttack = false;
    //        animator.SetTrigger("attack");
    //        Invoke("AllowToMove", .7f);
    //    }
    //}

    public void TrackTarget(Interactable interactable)
    {
        tracking = true;
        target = interactable.transform;
        agent.stoppingDistance = interactable.radius;
        agent.updateRotation = false;
    }

    public void StopTrackingTarget()
    {
        tracking = false;
        target = null;
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
    }

    public Interactable GetInteractable()
    {
        return interactable;
    }

}

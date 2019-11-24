using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    bool moving = false;
    float speed = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //AnimateMovement();
        CheckIfMoving();
    }

    private void AnimateMovement()
    {
        speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        animator.SetFloat("speed", speed, .1f, Time.deltaTime);
    }

    private void CheckIfMoving()
    {
        if (speed == 0)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }
}

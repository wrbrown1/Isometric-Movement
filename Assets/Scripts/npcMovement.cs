using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{

    Animator animator;
    [SerializeField][Range(0, 1)] float animationState;
    float automatedAnimationState = 0;
    bool increasing = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("idleState", automatedAnimationState);
        OscillateAnimationState();
    }

    private void PlayFootTap()
    {
        animator.SetFloat("blend", 1f);
        Invoke("ActNatural", 3f);
    }

    private void PlayLookAround()
    {
        animator.SetFloat("blend", 0.5f);
        Invoke("ActNatural", 3f);
    }

    private void ActNatural()
    {
        animator.SetFloat("blend", 0f);
    }

    private void OscillateAnimationState()
    {
        if (increasing)
        {
            automatedAnimationState += 0.08f * Time.deltaTime;
            if (automatedAnimationState >= 1)
            {
                increasing = false;
            }
        }
        else
        {
            automatedAnimationState -= 0.05f * Time.deltaTime;
            if (automatedAnimationState <= 0)
            {
                increasing = true;
            }
        }
    }
}

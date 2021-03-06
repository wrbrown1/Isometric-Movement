﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 combatCameraOffset;
    public Transform focusCameraPosition;
    public Transform defaultCameraPosition;
    public Transform target;
    public PlayerMovement player;
    public Interactable interactable;
    public float smoothSpeed;
    public float rotationSmoothSpeed;
    public float settleSpeed;
    public float cameraRotationSpeed = .5f;
    public float cameraApproachSpeed = 3f;
    bool engaging = false;
    bool settled = true;
    Quaternion startingRotation;
    Vector3 cameraPosition;

    private void Start()
    {
        startingRotation = transform.rotation;
        cameraPosition = transform.TransformPoint(new Vector3());
    }

    private void Update()
    {
        interactable = player.GetFocus();
    }

    void LateUpdate()
    {
        CheckIfEngaging(interactable);
        if (!engaging)
        {
            if (!settled)
            {
                MoveNormally();
            }
            Focus(target, false);
            SteadyDefaultCamera();
        }
    }

    private void SteadyDefaultCamera()
    {
        if(player.speed == 0f)
        {
            settled = true;
            float step = 1f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, (target.position + new Vector3(0, 8f, 7f)), smoothSpeed), settleSpeed);
        }
        else
        {
            settled = false;
        }
    }

    private void MoveNormally()
    {
        Vector3 desiredPosition = target.position + cameraPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void CheckIfEngaging(Interactable interactable)
    {
        if (interactable != null)
        {
            float distanceBetween = Vector3.Distance(player.transform.position, interactable.transform.position);
            if (distanceBetween <= interactable.radius + 2f)
            {
                if(interactable.type == 1)
                {
                    engaging = true;
                    Focus(interactable.transform, true);
                }
            }
        }
        else
        {
            engaging = false;
        }
    }

    private void Focus(Transform focus, bool engage)
    {
        if (engage)
        {
            Approach();
        }
        RotateTowards(focus);
    }

    private void Approach()
    {
        float step = cameraApproachSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, focusCameraPosition.position, step);
    }

    private void RotateTowards(Transform focus)
    {
        float rotationSpeed = cameraRotationSpeed * Time.deltaTime;
        Vector3 targetDirection = (focus.position - transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed, 0f);
        newDirection = Vector3.Lerp(targetDirection, newDirection, rotationSmoothSpeed);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void AdjustFOV()
    {
        if (Camera.main.fieldOfView < 70 && player.speed > 0f)
        {
            Camera.main.fieldOfView += 0.5f;
        }
        if (Camera.main.fieldOfView > 60 && player.speed == 0f)
        {
            Camera.main.fieldOfView -= 0.5f;
        }
    }
}

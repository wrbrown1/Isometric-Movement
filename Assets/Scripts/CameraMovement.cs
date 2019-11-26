using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 cameraOffset;
    public Vector3 combatCameraOffset;
    public Transform focusCameraPosition;
    public Transform defaultCameraPosition;
    public Transform target;
    public PlayerMovement player;
    public Interactable interactable;
    public float smoothSpeed;
    public float rotationSmoothSpeed;
    public float cameraRotationSpeed = .5f;
    public float cameraApproachSpeed = 3f;
    bool focusing = false;
    Quaternion startingRotation;
    Vector3 cameraPosition;

    private void Start()
    {
        startingRotation = transform.rotation;
        cameraPosition = transform.TransformPoint(cameraOffset);
    }

    private void Update()
    {
        interactable = player.GetFocus();
    }

    void LateUpdate()
    {
        CheckIfFocusing(interactable);
        if (!focusing)
        {
            MoveNormally();
            Focus(target);
        }
    }

    private void MoveNormally()
    {
        Vector3 desiredPosition = target.position + cameraPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
    }

    private void CheckIfFocusing(Interactable interactable)
    {
        if (interactable != null)
        {
            float distanceBetween = Vector3.Distance(player.transform.position, interactable.transform.position);
            if (distanceBetween <= interactable.radius + 2f)
            {
                focusing = true;
                Focus(interactable.transform);
            }
        }
        else
        {
            //float rotationSpeed = cameraRotationSpeed * Time.deltaTime;
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, startingRotation.eulerAngles, rotationSpeed, 0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
            //print("here");
            focusing = false;
        }
    }

    private void Focus(Transform focus)
    {
        Approach();
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
}

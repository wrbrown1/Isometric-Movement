using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 cameraOffset;
    public float smoothSpeed;
    [SerializeField] Transform target;
    public PlayerMovement player;
    public Interactable interactable;
    bool inCombat = false;

    private void Update()
    {
        interactable = player.GetInteractable();
        if (inCombat)
        {
            CheckForCombatInitiation(interactable);
        }
    }

    void LateUpdate()
    {
        if (!inCombat)
        {
            Vector3 desiredPosition = target.position + cameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    private void CheckForCombatInitiation(Interactable interactable)
    {
        if (interactable != null)
        {
            float distanceBetween = Vector3.Distance(player.transform.position, interactable.transform.position);
            if (distanceBetween <= interactable.radius + 2f)
            {
                inCombat = true;
                EngageCombat();
                //Camera.main.transform.LookAt(target);
                print("there");
            }
        }
    }

    private void EngageCombat()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}

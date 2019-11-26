using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 3f;
    public int type;
    bool isFocus = false;
    bool interacted = false;
    public Transform player;

    public virtual void Interact()
    {
        //Method meant to be overridden
    }

    void Update()
    {
        if (isFocus && !interacted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if(distance <= radius)
            {
                Interact();
                interacted = true;
            }
        }
    }

    public void OnFocused(Transform transform)
    {
        isFocus = true;
        player = transform;
        interacted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        interacted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}

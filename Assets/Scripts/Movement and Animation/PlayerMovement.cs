using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 cameraOffset;
    public LayerMask movementMask;
    public Interactable focus;
    public GameObject inventoryUI;
    public bool canMove = true;
    bool tracking = false;
    bool inCombat = false;
    public float speed;
    Interactable interactable;
    NavMeshAgent agent;
    Animator animator;
    Transform target;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckIfManagingInventory();
        GetSpeed();
        HandleMovement();
        AnimateMovement();
        CheckIfEngagingTarget();
    }

    private void CheckIfManagingInventory()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
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
        if (canMove)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                agent.destination = hit.point;
                StopTrackingTarget();
            }
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
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDeFocused();
            }
            focus = newFocus;
            TrackTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }

    private void StopFocusing()
    {
        if(focus != null)
        {
            focus.OnDeFocused();
        }
        focus = null;
        StopTrackingTarget();
    }

    public void TrackTarget(Interactable interactable)
    {
        tracking = true;
        target = interactable.transform;
        agent.stoppingDistance = interactable.radius;
        agent.updateRotation = false;
        if(interactable.radius > Vector3.Distance(interactable.transform.position, agent.transform.position))
        {
            transform.position -= interactable.transform.position;
        }
    }

    public void StopTrackingTarget()
    {
        tracking = false;
        target = null;
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
    }

    public Interactable GetFocus()
    {
        return focus;
    }

}

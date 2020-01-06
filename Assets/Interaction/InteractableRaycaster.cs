using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRaycaster : MonoBehaviour
{
    [SerializeField] private Transform source;
    [SerializeField] private float range = 100;

    private GameObject target;
    public GameObject Target
    { 
        get
        { 
            return target;
        }

        set
        {
            //Switch Detection
            if (target == value) return;
            //Switch Execution
            if (target != null) EndHovering(target);
            target = value;
            if (target != null) StartHovering(target);
        }
    }

    private void StartHovering(GameObject go)
    {
        foreach(IInteractable interactable in go.GetComponents(typeof(IInteractable)))
        {
            interactable.OnStartHover();
        }
    }

    private void EndHovering(GameObject go)
    {
        foreach (IInteractable interactable in go.GetComponents(typeof(IInteractable)))
        {
            interactable.OnEndHover();
        }
    }

    private void Interact(GameObject go)
    {
        foreach (IInteractable interactable in go.GetComponents(typeof(IInteractable)))
        {
            interactable.OnInteract();
        }
    }

    public void InteractWithTarget()
    {
        if (Target != null)
        {
            Interact(Target);
        }
    }

    private void Update()
    {
        //Scan every frame if there is a Target for that Raycaster
        Ray ray = new Ray(source.position, source.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Target = hit.collider.gameObject;
        } 
        else
        {
            Target = null;
        }
    }
}

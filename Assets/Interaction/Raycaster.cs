using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Transform source;
    [SerializeField] private float range;

    private IInteractable target;

    private void SetTarget(IInteractable target)
    {
        if (this.target != target)
        {
            if (this.target != null) this.target.OnEndHover();
            this.target = target;
            if (this.target != null) this.target.OnStartHover();
        }
    }

    private void Update()
    {
        Ray ray = new Ray(source.position, source.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null && interactable.MaxRange >= hit.distance)
            {
                SetTarget(interactable);
            }
            else
            {
                SetTarget(null);
            }
        }
    }
}

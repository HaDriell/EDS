using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Transform source;
    [SerializeField] private float range;

    private GameObject target;
    public GameObject Target
    {
        get 
        {
            return target; 
        }

        private set
        {
            EndTargetHover();
            target = value;
            StartTargetHover();
        }
    }

    private void EndTargetHover()
    {
        if (!target) return;
        foreach(IInteractable components in target.GetComponents(typeof(IInteractable)))
        {
            components.OnEndHover();
        }
    }

    private void StartTargetHover()
    {
        if (!target) return;
        foreach (IInteractable components in target.GetComponents(typeof(IInteractable)))
        {
            components.OnStartHover();
        }
    }

    private void Update()
    {
        Ray ray = new Ray(source.position, source.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
            Target = hit.collider.gameObject;
        else
            Target = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Transform interactionTransform;
    public float interactionRadius = 3f;

    bool hasInteracted = false;
    bool isFocus = false;
    Transform player;

    public void OnGainFocus(Transform transform)
    {
        hasInteracted = false;
        isFocus = true;
        player = transform;
    }

    public void OnLoseFocus()
    {
        hasInteracted = false;
        isFocus = false;
        player = null;
    }


    public virtual void OnInteract()
    {

    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < interactionRadius)
            {
                hasInteracted = true;
                OnInteract();
            }
        }
    }

    private void OnDrawGizmosSelected()
    { 
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
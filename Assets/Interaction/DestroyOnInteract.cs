using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInteract : MonoBehaviour, IInteractable
{
    public void OnEndHover() { }
    public void OnStartHover() { }

    public void OnInteract()
    {
        Destroy(gameObject);
    }

}

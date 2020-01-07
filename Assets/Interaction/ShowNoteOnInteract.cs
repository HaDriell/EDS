using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNoteOnInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Note note;

    public void OnEndHover()
    {
    }

    public void OnInteract()
    {
        NotePanelUI ui = FindObjectOfType<NotePanelUI>();

        if (note != null)
            ui.SetText(note.text);

        ui.Show();
    }

    public void OnStartHover()
    {
    }
}

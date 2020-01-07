using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Note", menuName = "Notes/Note")]
public class NotePanelUI : MonoBehaviour
{

    [SerializeField] private Image background;
    [SerializeField] private Text text;


    private void Awake()
    {
        Hide();
    }

    public void SetText(string messsage)
    {
        text.text = messsage;
    }

    public void Show()
    {
        text.enabled = true;
        background.enabled = true;
    }

    public void Hide()
    {
        text.enabled = false;
        background.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
}

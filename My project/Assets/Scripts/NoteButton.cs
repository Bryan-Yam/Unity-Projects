using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoteButton : Selectable, IPointerClickHandler
{
    public Sprite on_image;
    public Sprite off_image; 
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        active = !active;

        if (active)
            GetComponent<Image>().sprite = on_image;
        else
            GetComponent<Image>().sprite = off_image;

        GameEvents.on_notes_active_method(active);
    }
}

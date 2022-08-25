using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EraserButton : Selectable, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.on_erase_num_method();
    }
}

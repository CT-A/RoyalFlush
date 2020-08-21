using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonImg : Button, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject text;
    bool selected;
    public void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        text.SetActive(true);
        selected = true;
        //Debug.Log(this.gameObject.name + " was selected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        text.SetActive(false);
        selected = false;
        //Debug.Log(this.gameObject.name + " was deselected");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        text.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
        //Debug.Log(this.gameObject.name + " was selected");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!selected)
            text.SetActive(false);
        //Debug.Log(this.gameObject.name + " was deselected");
    }
}

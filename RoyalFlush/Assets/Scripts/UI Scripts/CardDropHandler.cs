using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropHandler : MonoBehaviour, IDropHandler
{
    public SwapHandler sh;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform card = transform as RectTransform;

        //dragged thing dropped in this card
        if (RectTransformUtility.RectangleContainsScreenPoint(card, Input.mousePosition)/* && (sh.heldCard != gameObject)*/)
        {
            sh.SwapCards(gameObject);
        }
    }
}

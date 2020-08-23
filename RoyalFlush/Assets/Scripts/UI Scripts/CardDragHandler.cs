using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 initPos;
    public SwapHandler sh;
    void Start()
    {
        initPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        sh.heldCard = gameObject;
        transform.position = Input.mousePosition;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initPos;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Overlay overlay;
    public void OnPointerEnter(PointerEventData data)
    {

    }
    public void OnPointerExit(PointerEventData data)
    {

    }

    public void OnDrop(PointerEventData data)
    {
        overlay.Play(data.pointerDrag);
    }
}

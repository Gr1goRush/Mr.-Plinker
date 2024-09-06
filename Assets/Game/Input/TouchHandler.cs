using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler :MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool PointerDown { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerDown = false;
    }
}
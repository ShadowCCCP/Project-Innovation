using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldDownButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public bool IsPressed()
    {
        return buttonPressed;
    }
}
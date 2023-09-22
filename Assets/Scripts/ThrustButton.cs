using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThrustButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite downImage;
    public Sprite upImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        Movement.Instance.isThrusting = true;
        this.gameObject.GetComponent<Image>().sprite = downImage;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Movement.Instance.isThrusting = false;
        this.gameObject.GetComponent<Image>().sprite = upImage;
    }
}

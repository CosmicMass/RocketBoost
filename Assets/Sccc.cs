using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sccc : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite downImage;
    public Sprite upImage;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("True" + Movement.Instance.isThrusting);
        Movement.Instance.isThrusting = true;
        this.gameObject.GetComponent<Image>().sprite = downImage;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("False" + Movement.Instance.isThrusting);
        Movement.Instance.isThrusting = false;
        this.gameObject.GetComponent<Image>().sprite = upImage;
    }
}

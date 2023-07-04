using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    public Transform item;
    public Transform rightHand;
    public void TakeItemToEat()
    {
        item.GetComponent<ObjectPuzzle>().SetPositionToEat();
        item.parent = rightHand;
        item.localPosition = Vector3.zero;
        item.localEulerAngles = Vector3.zero;
    }

    public void EndEat()
    {
        item.gameObject.SetActive(false);
    }
}

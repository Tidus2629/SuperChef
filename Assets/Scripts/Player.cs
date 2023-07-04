using DigitalRubyShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{

    public ObjectInteract currentObject;
    public float timeToControl;


    private float currentTimeToControl;
    private bool isControl;
    private float timeLongPress;
    private bool isCheckedLongPress;
    private Vector2 originPos;
    private bool canControlObject;
    private int indexCurrentObject = 0;
    public Hand hand;
    public bool needAim = true;
    private Vector2 originPosControlHand;
    public float minimumLongPressDuration = 0;

    private void Start()
    {
        ControllManager.Instance.OnBeginHold += OnBeginHold;
        ControllManager.Instance.OnHoldDragEnd += EndHoldDrag;
        ControllManager.Instance.OnDrag += OnDrag;
        ControllManager.Instance.longPress.MinimumDurationSeconds = minimumLongPressDuration;
        if (needAim)
        {
            GameManager.Instance.type = 3;
            hand.lineAim.gameObject.SetActive(true);
        }
        else if (!needAim)
        {
            GameManager.Instance.type = 1;

        }
    }

    private void OnDestroy()
    {
        ControllManager.Instance.OnBeginHold -= OnBeginHold;
        ControllManager.Instance.OnHoldDragEnd -= EndHoldDrag;
        ControllManager.Instance.OnDrag -= OnDrag;
    }

    public void SelectObject(int _index)
    {
        if (LevelManager.Instance.listObject.Count == 0)
            return;
        if (_index < 0)
            _index = LevelManager.Instance.listObject.Count - 1;
        else if (_index >= LevelManager.Instance.listObject.Count)
            _index = 0;

        if (indexCurrentObject < LevelManager.Instance.listObject.Count && indexCurrentObject >= 0)
            LevelManager.Instance.listObject[indexCurrentObject].DeSelected();
        indexCurrentObject = _index;
        LevelManager.Instance.listObject[indexCurrentObject].OnSelected();
        currentObject = LevelManager.Instance.listObject[indexCurrentObject];
    }

    public void SelectObject(ObjectInteract _object)
    {
        if (LevelManager.Instance.listObject.Count == 0)
            return;
        if (currentObject != null)
            currentObject.DeSelected();
        indexCurrentObject = LevelManager.Instance.listObject.IndexOf(_object);
        LevelManager.Instance.listObject[indexCurrentObject].OnSelected();
        currentObject = LevelManager.Instance.listObject[indexCurrentObject];

    }

    public void NoSelectObject()
    {
        if (currentObject != null)
        {
            currentObject.DeSelected();
            currentObject = null;
        }
    }

    private void EndHoldDrag(GestureRecognizer gesture)
    {
        if (!hand.gameObject.activeSelf)
            return;
        currentTimeToControl = 0;

        if (GameManager.Instance.type == 3)
        {
            hand.lineAim.gameObject.SetActive(true);
        }

        if (isControl)
        {
            currentObject.EndControl(gesture);
            isControl = false;
        }

        canControlObject = false;
        isCheckedLongPress = false;
        hand.EndHold();
        LevelManager.Instance.player.NoSelectObject();

    }

    private void OnBeginHold(GestureRecognizer gesture)
    {
        if (!hand.gameObject.activeSelf)
            return;

        if (needAim)
        {
            originPos = new Vector2(gesture.FocusX, gesture.FocusY);
            originPosControlHand = new Vector2(gesture.FocusX, gesture.FocusY);
            timeLongPress = Time.unscaledTime;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 15))
            {
                ObjectInteract ob = hit.collider.GetComponent<ObjectInteract>();
                if (ob != null && LevelManager.Instance.listObject.Contains(ob))
                {
                    hand.transform.LookAt(hit.point);
                    hand.AimOnTap(hit.point);
                    LevelManager.Instance.player.SelectObject(ob);
                    StartControlObject();
                    isControl = true;
                }
            }
        }
    }

    public void OnDrag(GestureRecognizer gesture)
    {
        if (!hand.gameObject.activeSelf)
            return;
        if (GameManager.Instance.type != 3)
        {
            if (!isControl)
            {
                if (currentObject != null)
                {
                    currentTimeToControl += Time.unscaledDeltaTime;
                    if (currentTimeToControl >= timeToControl)
                    {
                        StartControlObject();
                    }
                }
            }
            if (isControl)
            {
                ControlObject(gesture);
                hand.AimWhenControl(currentObject.transform);
            }
            return;
        }
        else if (GameManager.Instance.type == 3)
        {

            if (!isCheckedLongPress)
            {
                if (Time.unscaledTime - timeLongPress >= 0.5f)
                {

                    if (currentObject != null)
                    {
                        canControlObject = IsHoldToControl(gesture);
                    }
                }
            }
            if (!canControlObject)
            {
                float angle = (originPosControlHand.x - gesture.FocusX) * 0.15f;
                float angle2 = (originPosControlHand.y - gesture.FocusY) * 0.15f;


                if (angle < 180 && angle > 45)
                    angle = 45;
                if (angle < -45 || (angle > 180 && angle < 315))
                    angle = -45;
                if (angle2 < 180 && angle2 > 45)
                    angle2 = 45;
                if (angle2 < -45 || (angle2 > 180 && angle2 < 315))
                    angle2 = -45;

                hand.Aim(new Vector3(angle2, -angle, 0));
            }
            else if (canControlObject)
            {

                if (!isControl)
                {
                    if (Math.Abs((int)originPos.x - (int)gesture.FocusX) > 10 || Math.Abs((int)originPos.y - (int)gesture.FocusY) > 10)
                    {
                        LostControl(gesture);
                        hand.EndHold();
                        return;
                    }
                    ShowControlUI();
                    currentTimeToControl += Time.unscaledDeltaTime;
                    if (currentTimeToControl >= timeToControl)
                    {
                        StartControlObject();
                    }
                }
                if (isControl)
                {
                    ControlObject(gesture);

                    float angle = (originPosControlHand.x - gesture.FocusX) * 0.05f;
                    float angle2 = (originPosControlHand.y - gesture.FocusY) * 0.05f;

                    if (angle < 180 && angle > 45)
                        angle = 45;
                    if (angle < -45 || (angle > 180 && angle < 315))
                        angle = -45;
                    if (angle2 < 180 && angle2 > 45)
                        angle2 = 45;
                    if (angle2 < -45 || (angle2 > 180 && angle2 < 315))
                        angle2 = -45;
                    hand.AimWhenControl(currentObject.transform);
                }
            }
        }
    }

    private void ShowControlUI()
    {
        if (currentTimeToControl == 0)
        {
            hand.Hold();
        }
    }

    private void StartControlObject()
    {
        isControl = true;
        currentObject.BeginControl();
        hand.Hold();
        if (GameManager.Instance.type == 3)
        {
            hand.lineAim.gameObject.SetActive(false);
        }
        hand.target = currentObject.transform;
    }

    private void ControlObject(GestureRecognizer gesture)
    {
        if (GameManager.Instance.type == 3)
        {
            hand.lineAim.gameObject.SetActive(false);
        }
        currentObject.OnControl(gesture);
        hand.OnHold(gesture);
    }

    private bool IsHoldToControl(GestureRecognizer gesture)
    {

        if (Math.Abs((int)originPos.x - (int)gesture.FocusX) < 10 && Math.Abs((int)originPos.y - (int)gesture.FocusY) < 10)
        {
            isCheckedLongPress = true;
            originPos = new Vector2(gesture.FocusX, gesture.FocusY);
            return true;
        }
        else if (Math.Abs((int)originPos.x - (int)gesture.FocusX) > 10 || Math.Abs((int)originPos.y - (int)gesture.FocusY) > 10)
        {
            timeLongPress = Time.unscaledTime;
            originPos = new Vector2(gesture.FocusX, gesture.FocusY);
            return false;
        }
        return false;
    }

    private void LostControl(GestureRecognizer gesture)
    {
        timeLongPress = Time.unscaledTime;
        isCheckedLongPress = false;
        originPos = new Vector2(gesture.FocusX, gesture.FocusY);
        canControlObject = false;
        currentTimeToControl = 0;
    }

    
}

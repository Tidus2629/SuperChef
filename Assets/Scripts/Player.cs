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
       // ControllManager.Instance.OnTap += TapObject;
        ControllManager.Instance.OnBeginHold += OnBeginHold;
        ControllManager.Instance.OnHoldDragEnd += EndHoldDrag;
        ControllManager.Instance.OnDrag += OnDrag;
        ControllManager.Instance.longPress.MinimumDurationSeconds = minimumLongPressDuration;
        //ControllManager.Instance.OnSwipe += OnSwipe;
        if (needAim)
        {
            GameManager.Instance.type = 3;
            hand.lineAim.gameObject.SetActive(true);
            //ControllManager.Instance.longPress.MinimumDurationSeconds = minimumLongPressDuration;
        }
        else if (!needAim)
        {
            GameManager.Instance.type = 1;
            // SelectObject(indexCurrentObject);
        }
        //   SelectObject(indexCurrentObject);
    }

    private void OnDestroy()
    {
        //  ControllManager.Instance.OnSwipe -= ChangeObject;
       // ControllManager.Instance.OnTap -= TapObject;
        ControllManager.Instance.OnBeginHold -= OnBeginHold;
        // ControllManager.Instance.OnHold -= HoldOnObject;
        ControllManager.Instance.OnHoldDragEnd -= EndHoldDrag;
        ControllManager.Instance.OnDrag -= OnDrag;
        //ControllManager.Instance.OnSwipe -= OnSwipe;
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
        if (GameManager.Instance.type != 3)
        {
            //hand.RotateToTarget(currentObject.transform);
        }
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
        if (GameManager.Instance.type != 3)
        {
            // hand.RotateToTarget(currentObject.transform);
        }
    }

    public void NoSelectObject()
    {
        if (currentObject != null)
        {
            currentObject.DeSelected();
            currentObject = null;
        }
    }

    private void ChangeObject(SwipeGestureRecognizerDirection _dir)
    {
        if (GameManager.Instance.type != 2)
        {
            return;
        }

        if (_dir == SwipeGestureRecognizerDirection.Left)
        {
            SelectObject(indexCurrentObject - 1);
        }
        else if (_dir == SwipeGestureRecognizerDirection.Right)
        {
            SelectObject(indexCurrentObject + 1);
        }
    }

    private void ChangeObject()
    {

        if (GameManager.Instance.type != 1)
        {
            return;
        }
        SelectObject(indexCurrentObject + 1);
    }

    private void TapObject()
    {

        if (!hand.gameObject.activeSelf)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 7))
        {
            ObjectInteract ob = hit.collider.GetComponent<ObjectInteract>();
            if (ob != null && ob != LevelManager.Instance.player.currentObject && LevelManager.Instance.listObject.Contains(ob))
            {
                hand.transform.LookAt(hit.point);
                hand.AimOnTap(hit.point);
                LevelManager.Instance.player.SelectObject(ob);
            }
        }
    }

    private void OnSwipe(GestureRecognizer gesture)
    {
        if (currentObject != null)
        {
            Vector3 dir = new Vector3(gesture.DeltaX, gesture.DeltaY, 0);
            currentObject.OnSwipe(dir);

        }
    }

    private void EndHoldDrag(GestureRecognizer gesture)
    {
        if (!hand.gameObject.activeSelf)
            return;
        currentTimeToControl = 0;

        //if (listThrowObject.Count > 0)
        //{
        //    pointThrow = hand.lineAim.GetPosition(1);
        //    hand.mAnimator.SetTrigger("Throw");
        //    Invoke("ThrowObject", 0.5f);
        //}


        if (GameManager.Instance.type == 3)
        {
            hand.lineAim.gameObject.SetActive(true);
        }

        if (isControl)
        {
            currentObject.EndControl(gesture);
            isControl = false;
        }
        //  SelectObject(indexCurrentObject);
        //   GUIManager.Instance.HideTimeHold();
        canControlObject = false;
         isCheckedLongPress = false;

        hand.EndHold();
        //hand.lineAim.gameObject.SetActive(false);
        LevelManager.Instance.player.NoSelectObject();
        // Time.timeScale = 1;

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

        //originPos = new Vector2(gesture.FocusX, gesture.FocusY);
        //originPosControlHand = new Vector2(gesture.FocusX, gesture.FocusY);

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 15))
        //{
        //    ObjectInteract ob = hit.collider.GetComponent<ObjectInteract>();
        //    if (ob != null && LevelManager.Instance.listObject.Contains(ob))
        //    {
        //        hand.transform.LookAt(hit.point);
        //        hand.AimOnTap(hit.point);
        //        LevelManager.Instance.player.SelectObject(ob);
        //        StartControlObject();
        //        isControl = true;
        //    }
        //}
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
                    ShowControlUI();
                    // Time.fixedDeltaTime
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
                // ShowAim(gesture);
                float angle = (originPosControlHand.x - gesture.FocusX) * 0.15f;
                float angle2 = (originPosControlHand.y - gesture.FocusY) * 0.15f;

                //   float angle = gesture.FocusX * 0.15f;
                // float angle2 = gesture.FocusX * 0.15f;

                if (angle < 180 && angle > 45)
                    angle = 45;
                if (angle < -45 || (angle > 180 && angle < 315))
                    angle = -45;
                if (angle2 < 180 && angle2 > 45)
                    angle2 = 45;
                if (angle2 < -45 || (angle2 > 180 && angle2 < 315))
                    angle2 = -45;

                //hand.transform.localEulerAngles = new Vector3(angle2, -angle, 0);
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

                    //   float angle = gesture.FocusX * 0.15f;
                    // float angle2 = gesture.FocusX * 0.15f;

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

        //Vector3 vt = Camera.main.WorldToScreenPoint(hand.lineAim.GetPosition(1));
        //// vt.x = vt.x - Screen.width / 2;
        ////  vt.y = vt.y - Screen.height / 2;

        //GUIManager.Instance.ShowTimeHold(currentTimeToControl / timeToControl, hand.lineAim.GetPosition(1));
    }

    private void StartControlObject()
    {
        //GUIManager.Instance.HideTimeHold();
        isControl = true;
        currentObject.BeginControl();
        hand.Hold();
        if (GameManager.Instance.type == 3)
        {
            hand.lineAim.gameObject.SetActive(false);
        }
        hand.target = currentObject.transform;

        //isControl = true;
        //currentObject.BeginControl();
        //// hand.Hold();
        ////if (GameManager.Instance.type == 3)
        ////{
        ////   // hand.lineAim.gameObject.SetActive(false);
        ////}
        //hand.target = currentObject.transform;
        //hand.Hold();
        //hand.AimWhenControl(currentObject.transform);


    }

    private void ControlObject(GestureRecognizer gesture)
    {
        if (GameManager.Instance.type == 3)
        {
            hand.lineAim.gameObject.SetActive(false);
        }
        currentObject.OnControl(gesture);
        hand.OnHold(gesture);

        //currentObject.OnControl(gesture);
        //hand.OnHold(gesture);
        //hand.OnControllObject(currentObject.transform.position, currentObject.transform.position);
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

    public void StopMove()
    {
        transform.DOKill();
    }

    public void MoveToNextLevel(float _time = 3)
    {
        hand.lineAim.gameObject.SetActive(false);
        //if (posToNextLevel.z != 0)
        //{
        //    hand.Move();
        //    transform.DOMoveY(transform.position.y + 0.08f, 0.3f).SetEase(Ease.OutCubic).SetLoops(8, LoopType.Yoyo);
        //    transform.DORotate(Vector3.zero, 1);
        //}

        //transform.DOMoveZ(transform.position.z + posToNextLevel.z, _time).OnComplete(() =>
        //     {
        //         LevelManager.Instance.LevelWin();
        //     }
        //);

    }
}

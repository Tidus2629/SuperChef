using DigitalRubyShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllManager : MonoBehaviour
{
    public static ControllManager Instance;

    private SwipeGestureRecognizer swipe;
    public LongPressGestureRecognizer longPress;
    private TapGestureRecognizer tap;

    private float timeLongPress;
    private bool isCheckedLongPress;
    private bool isHold;
    private bool isDrag;

    public Action<GestureRecognizer> OnSwipe;
    public Action OnHold;
    public Action<GestureRecognizer> OnDrag;
    public Action<GestureRecognizer> OnHoldDragEnd;
    public Action OnTap;
    public Action<GestureRecognizer> OnBeginHold;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        CreateGesture();
    }

    private void Start()
    {

    }

    public void CreateGesture()
    {
        CreateTapGesture();
        CreateLongPressGesture();
        CreateSwipeGesture();
    }

    private void CreateTapGesture()
    {
        tap = new TapGestureRecognizer();
        tap.StateUpdated += TapCallBack;
        tap.ThresholdSeconds = 0.2f;
        tap.AllowSimultaneousExecutionWithAllGestures();
        FingersScript.Instance.AddGesture(tap);
    }

    private void TapCallBack(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Began)
        {
        }

        if (gesture.State == GestureRecognizerState.Ended)
        {
            if (OnTap != null)
            {
                OnTap();
            }

        }
    }

    private void CreateLongPressGesture()
    {
        longPress = new LongPressGestureRecognizer();
        longPress.StateUpdated += LongPressCallBack;
        longPress.MinimumDurationSeconds = 0.3f;
        //FingersScript.
        FingersScript.Instance.AddGesture(longPress);
    }

    private void LongPressCallBack(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Began)
        {
            if (OnBeginHold != null)
            {
                OnBeginHold(gesture);
            }
        }
        else if (gesture.State == GestureRecognizerState.Executing)
        {

            //if (!isCheckedLongPress)
            //{
            //    if (Time.time - timeLongPress >= 0.2f)
            //    {
            //        if (Math.Abs((int)gesture.FocusX - (int)gesture.StartFocusX) < 10 && Math.Abs((int)gesture.FocusY - (int)gesture.StartFocusY) < 10)
            //        {
            //            isHold = true;
            //            isCheckedLongPress = true;
            //        }
            //    }
            //    else if (Math.Abs((int)gesture.FocusX - (int)gesture.StartFocusX) > 10 || Math.Abs((int)gesture.FocusY - (int)gesture.StartFocusY) > 10)
            //    {
            //        isDrag = true;
            //        isCheckedLongPress = true;
            //    }
            //}

            //if (isHold)
            //{
            //    // Debug.Log("BBBBBBBBBBBB");
            //    if (OnHold != null)
            //        OnHold();
            //}

            //else if (isDrag)
            //{
            if (OnDrag != null)
                OnDrag(gesture);
            //}
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            if (OnHoldDragEnd != null)
                OnHoldDragEnd(gesture);
        }
    }

    private void CreateSwipeGesture()
    {
        swipe = new SwipeGestureRecognizer();
        swipe.StateUpdated += SwipeCallBack;
        swipe.AllowSimultaneousExecutionWithAllGestures();
        //swipe.tim
        FingersScript.Instance.AddGesture(swipe);
    }

    private void SwipeCallBack(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            if (OnSwipe != null)
            {
                OnSwipe(gesture); ;
            }
        }
    }

    //private float timeTouch;
    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        timeTouch = Time.time;
    //    }  

    //    else if(input)
    //}
}

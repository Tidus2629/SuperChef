using DG.Tweening;
using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ketchap : ObjectInteract
{
    private bool canDrag;
    private Vector3 originPoint;
    public GameObject fluid;
    private bool isDragging;
    public float timeToFlush;
    private float currentTimeToFlush;
    private float currentForceFlush;
    private void Start()
    {
        currentTimeToFlush = timeToFlush;
    }

    public override void Active()
    {
        base.Active();
        gameObject.SetActive(false);
    }

    public void Deactive()
    {
        mRigidbody.useGravity = false;
        mRigidbody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public override void BeginControl()
    {
        transform.DOMove(transform.position + new Vector3(0, 0.1f, 0), 0.1f).OnComplete(() =>
        {
            base.BeginControl();
            canDrag = true;
        });
        mRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        transform.DORotateQuaternion(initRotate, 0.2f);
        fluid.SetActive(true);
        mAnimator.SetTrigger("Flush");
        isDragging = true;
    }


    public override void OnControl(GestureRecognizer gesture)
    {
        if (canDrag)
            base.OnControl(gesture);
        originPoint = Input.mousePosition;
    }

    private void Update()
    {
        if (isDragging)
        {
            currentTimeToFlush -= Time.deltaTime;
            currentForceFlush += Time.deltaTime;
            mAnimator.SetFloat("ForceFlush", currentForceFlush);
            if (currentTimeToFlush < 0)
            {
                currentTimeToFlush = 0;
                isDragging = false;
                LevelManager.Instance.ShowResult();
                fluid.transform.parent = null;
                gameObject.SetActive(false);
            }

        }
    }

    public override void EndControl(GestureRecognizer gesture)
    {
        ObjectPuzzle.waitCheckCorrect = true;
        canDrag = false;
        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;
        isOnControl = false;
        mRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        Vector3 dir = new Vector3(gesture.DeltaX, gesture.DeltaY, 0);
        if (dir.magnitude > 10)
        {
            mRigidbody.AddForce(dir.normalized * 5f, ForceMode.Impulse);
        }
        else
        {
            mRigidbody.AddForce(Vector3.down * 2f, ForceMode.Impulse);
        }
    }
}
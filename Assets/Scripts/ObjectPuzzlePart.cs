using DG.Tweening;
using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuzzlePart : ObjectInteract
{

    public GameObject shadowObject;
    public ObjectPuzzle mainObject;
    //public List<Transform> otherPart;
    //public List<Transform> other
    private bool isDroped;
    private bool needCheckCorrect;
    private bool canDrag;
    private Vector3 originPoint;
    private void Start()
    {
        initRotate = shadowObject.transform.rotation;
    }

    public void Deactive()
    {
        mRigidbody.useGravity = false;
        mRigidbody.isKinematic = true;
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
        needCheckCorrect = false;
    }


    public override void OnControl(GestureRecognizer gesture)
    {
        if (canDrag)
            base.OnControl(gesture);
        originPoint = Input.mousePosition;
    }

    private void Update()
    {
        if (needCheckCorrect)
        {
            if (mRigidbody.velocity.magnitude <= 0 && mRigidbody.angularVelocity.magnitude <= 0)
            {
                mainObject.CheckComplete();
                needCheckCorrect = false;
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
        isDroped = true;
        Vector3 dir = new Vector3(gesture.DeltaX, gesture.DeltaY, 0);
        if (dir.magnitude > 10)
        {
            mRigidbody.AddForce(dir.normalized * 5f, ForceMode.Impulse);
        }
        else
        {
            mRigidbody.AddForce(Vector3.down * 2f, ForceMode.Impulse);
        }
        needCheckCorrect = true;
        ObjectPuzzle.waitCheckCorrect = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDroped && !isOnControl)
        {
            isDroped = false;
        }
    }
}

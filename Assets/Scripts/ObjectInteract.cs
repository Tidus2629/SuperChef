using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class ObjectInteract : MonoBehaviour
{
    //public Outline outline;
    public HighlightEffect highlight;
    public Animator mAnimator;
    public Rigidbody mRigidbody;
    public AudioSource soundPickAndDrop;

    //public float timeToControl;

    //private float currentTime;

    protected Vector3 screenPoint;
    protected Vector3 offset;
    public Quaternion initRotate;
    public float pointZ;
    public bool isOnControl;

    public virtual void OnSelected()
    {
        if (highlight != null)
            highlight.highlighted = true;
    }

    public virtual void DeSelected()
    {
        if (highlight != null)
            highlight.highlighted = false;
    }

    public virtual void BeginControl()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * 2, Input.mousePosition.y * 2, screenPoint.z));
        isOnControl = true;
        soundPickAndDrop.Play();
    }



    public virtual void BeginControl(Vector3 _pos)
    {
        screenPoint = Camera.main.WorldToScreenPoint(_pos);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x * 2, Input.mousePosition.y * 2, screenPoint.z));
        isOnControl = true;
        if (soundPickAndDrop != null)
            soundPickAndDrop.Play();

    }

    public virtual void OnControl(GestureRecognizer gesture)
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x * 2, Input.mousePosition.y * 2, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        cursorPosition.z = pointZ;
        mRigidbody.velocity = (cursorPosition - transform.position) * 5;
    }


    public virtual void EndControl(GestureRecognizer gesture)
    {
        mRigidbody.useGravity = true;
        mRigidbody.isKinematic = false;
        isOnControl = false;
        mRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        mRigidbody.velocity = Vector3.zero;
        Vector3 dir = new Vector3(gesture.DeltaX, gesture.DeltaY, 0);
        if (dir.magnitude > 10)
        {
            mRigidbody.AddForce(dir.normalized * 15f, ForceMode.Impulse);
        }
    }

    public virtual void Active()
    {

    }

    public virtual void OnTap()
    {

    }

    public virtual void OnSwipe(Vector3 _dir)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // soundPickAndDrop.Play();
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    public Animator mAnimator;
    public GameObject effectHold;
    public Transform target;
    public LineRenderer lineAim;
    public LineControl lineControll;

    private void Start()
    {
        lineAim.SetPosition(0, lineAim.transform.position);
        lineAim.SetPosition(1, lineAim.transform.forward * 5);
    }

    public void Hold()
    {
        mAnimator.SetTrigger("Hold");
        effectHold.SetActive(true);
        mAnimator.SetFloat("X", 0);
        mAnimator.SetFloat("Y", 0);
    }

    public void OnHold(GestureRecognizer gesture)
    {
        // transform.LookAt(target);
        Vector2 vt = new Vector2(gesture.VelocityX / 1000, gesture.VelocityY / 1000);
        //  vt.Normalize();
        // Debug.Log(vt);
        mAnimator.SetFloat("X", vt.x);
        mAnimator.SetFloat("Y", vt.y);
    }

    public void EndHold()
    {
        mAnimator.SetTrigger("Idle");
        effectHold.SetActive(false);
        lineAim.SetPosition(0, lineAim.transform.position);
        lineAim.SetPosition(1, lineAim.transform.forward * 5);
        //mAnimator.SetTrigger("Idle");
        //effectHold.SetActive(false);
        //lineAim.SetPosition(0, lineAim.transform.position);
        //lineAim.SetPosition(1, lineAim.transform.position);
        //lineAim.SetPosition(2, lineAim.transform.position);
    }

    public void RotateToTarget(Transform _target)
    {
        target = _target;
        Quaternion rot = Quaternion.LookRotation(target.position - transform.position);
        //transform.DORotateQuaternion(rot, 0.2f);
        transform.DORotateQuaternion(rot, 0.3f);
    }

    public void Move()
    {
        mAnimator.SetTrigger("Move");
    }

    public void AimOnTap(Vector3 _pos)
    {
        lineAim.SetPosition(0, lineAim.transform.position);
        lineAim.SetPosition(1, _pos);
    }

    public void Aim(Vector3 _pointAim)
    {
        transform.localEulerAngles = _pointAim;
        RaycastHit hit;
        Vector3 vt = lineAim.transform.forward * 7;
        if (Physics.Raycast(lineAim.transform.position, vt - lineAim.transform.position, out hit, 7))
        {
            lineAim.SetPosition(0, lineAim.transform.position);
            lineAim.SetPosition(1, hit.point);
            ObjectInteract ob = hit.collider.GetComponent<ObjectInteract>();
            if (ob != null && ob != LevelManager.Instance.player.currentObject && LevelManager.Instance.listObject.Contains(ob))
            {
                // transform.position = hit.point;
                Debug.Log(ob.name);
                LevelManager.Instance.player.SelectObject(ob);
            }
            else if (ob == null)
            {
                LevelManager.Instance.player.NoSelectObject();
            }
        }
        else
        {
            LevelManager.Instance.player.NoSelectObject();
            lineAim.SetPosition(0, lineAim.transform.position);
            lineAim.SetPosition(1, lineAim.transform.forward * 7);
        }
    }

    //private void Update()
    //{

    //}


    public void OnControllObject(Vector3 _pointJoint, Vector3 _pointObject)
    {
        //lineAim.gameObject.SetActive(true);
        //lineAim.SetPosition(0, lineAim.transform.position);
        //lineAim.SetPosition(1, _pointJoint);
        //lineAim.SetPosition(2, _pointObject);
        lineControll.SetPoint(lineAim.transform.position, _pointJoint, _pointObject);
    }

    public void AimWhenControl(Transform _target)
    {
        transform.LookAt(_target);
        //transform.localEulerAngles = _pointAim;
    }

}

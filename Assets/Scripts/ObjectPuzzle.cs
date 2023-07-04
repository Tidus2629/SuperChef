using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuzzle : MonoBehaviour
{
    public float pointAccuracy;
    public List<ObjectPuzzlePart> listPart;
    public List<Transform> listPartCorrect;
    private bool isDone;
    public static bool waitCheckCorrect;

    private void CompleteObject()
    {
        for (int i = 0; i < listPart.Count; i++)
        {
            listPart[i].Deactive();
        }
        if (pointAccuracy >= 5)
            LevelManager.Instance.SetStar(3);
        else if (pointAccuracy >= 4.5f && pointAccuracy < 5)
            LevelManager.Instance.SetStar(2);
        else if (pointAccuracy < 4.5)
            LevelManager.Instance.SetStar(1);

        //LevelManager.Instance.ShowKetchap();
        LevelManager.Instance.ShowResult();

    }

    public void CheckComplete()
    {
        if (isDone)
            return;
        else
        {
            //StopCoroutine(CoCheckComplete());
            //StartCoroutine(CoCheckComplete());
            // CancelInvoke("CoCheckComplete");
            // Invoke("CoCheckComplete", 2f);
            CoCheckComplete();
        }
    }

    private void CoCheckComplete()
    {
        //  yield return new WaitForSeconds(2);
        if (IsCorrect())
        {
            isDone = true;
            // GetComponent<AudioSource>().Play();
            CompleteObject();
        }
        // StopCoroutine(CoCheckComplete());
    }

    private bool IsCorrect()
    {
        for (int i = 0; i < listPart.Count; i++)
        {
            for (int j = i + 1; j < listPart.Count; j++)
            {
                float dis = Vector3.Distance(listPart[i].transform.position, listPart[j].transform.position);
                float angle = Vector3.Angle(listPart[i].transform.position, listPart[j].transform.position);

                Vector3 dir = listPart[i].transform.position - listPart[j].transform.position;
                Vector3 dir_2 = listPartCorrect[i].transform.position - listPartCorrect[j].transform.position;


                float dis_2 = Vector3.Distance(listPartCorrect[i].transform.position, listPartCorrect[j].transform.position);
                float angle_2 = Vector3.Angle(listPartCorrect[i].transform.position, listPartCorrect[j].transform.position);

                float checkDis = Mathf.Abs(dis - dis_2);
                if (checkDis >= 0 && checkDis < 0.008f)
                {
                    pointAccuracy += 1;
                }
                else if (checkDis >= 0.008f && checkDis < 0.016f)
                {
                    pointAccuracy += 0.5f;
                }
                else if (checkDis >= 0.016f && checkDis < 0.032f)
                {
                    pointAccuracy += 0.1f;
                }
                else if (checkDis >= 0.032f)
                {
                    pointAccuracy = 0;
                    waitCheckCorrect = false;
                    return false;
                }

                float checkAngle = Mathf.Abs(angle - angle_2);
                if (checkAngle >= 0 && checkDis < 1.65f)
                {
                    pointAccuracy += 1;
                }
                else if (checkAngle >= 1.65f && checkDis < 3f)
                {
                    pointAccuracy += 0.5f;
                }
                else if (checkAngle >= 3f && checkDis < 4f)
                {
                    pointAccuracy += 0.1f;
                }
                else if (checkAngle >= 4f)
                {
                    pointAccuracy = 0;
                    waitCheckCorrect = false;
                    return false;
                }

                ////float checkDirection = Vector3.Dot(dir.normalized, dir_2.normalized);
                ////if (checkDirection >= 0 && checkDirection < 1.65f)
                ////{
                ////    pointAccuracy += 1;
                ////}
                ////else if (checkDirection >= 1.65f && checkDirection < 3f)
                ////{
                ////    pointAccuracy += 0.5f;
                ////}
                ////else if (checkDirection >= 3f && checkDirection < 4f)
                ////{
                ////    pointAccuracy += 0.1f;
                ////}
                ////else if (checkDirection >= 4f)
                ////{
                ////    pointAccuracy = 0;
                ////    waitCheckCorrect = false;
                ////    return false;
                ////}


                ////if (Vector3.Dot(dir.normalized, dir_2.normalized) < 0.8f)
                ////{
                ////    pointAccuracy = 0;
                ////    waitCheckCorrect = false;
                ////    return false;
                ////}
                ////pointAccuracy += 1;

            }
            //float angle_3 = 1 - Mathf.Abs(Quaternion.Dot(listPart[i].transform.rotation, listPartCorrect[i].transform.rotation));
            //if (angle_3 >= 0.1f)
            //{
            //    pointAccuracy = 0;
            //    waitCheckCorrect = false;
            //    return false;
            //}
            //pointAccuracy += 1;
        }
        waitCheckCorrect = false;
        return true;
    }

    private void Start()
    {
        waitCheckCorrect = false;
        for (int i = 0; i < listPart.Count; i++)
        {
            for (int j = i + 1; j < listPart.Count; j++)
            {
                float dis = Vector3.Distance(listPart[i].transform.position, listPart[j].transform.position);
                float angle = Vector3.Angle(listPart[i].transform.position, listPart[j].transform.position);

                float dis_2 = Vector3.Distance(listPartCorrect[i].transform.position, listPartCorrect[j].transform.position);
                float angle_2 = Vector3.Angle(listPartCorrect[i].transform.position, listPartCorrect[j].transform.position);

                //Debug.Log(dis + ":" + dis_2);
                //Debug.Log(angle + ":" + angle_2);
            }
        }
    }

    public void SetPositionToEat()
    {
        for (int i = 1; i < listPart.Count; i++)
        {
            listPart[i].transform.parent = listPart[0].transform;
        }
        listPart[0].transform.localPosition = Vector3.zero;
    }
}

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

          LevelManager.Instance.ShowResult();

    }

    public void CheckComplete()
    {
        if (isDone)
            return;
        else
        {
           
            CoCheckComplete();
        }
    }

    private void CoCheckComplete()
    {
        if (IsCorrect())
        {
            isDone = true;
            CompleteObject();
        }
    }

    private bool IsCorrect()
    {
        for (int i = 0; i < listPart.Count; i++)
        {
            for (int j = i + 1; j < listPart.Count; j++)
            {
                float dis = Vector3.Distance(listPart[i].transform.position, listPart[j].transform.position);
                float angle = Vector3.Angle(listPart[i].transform.position, listPart[j].transform.position);

                float dis_2 = Vector3.Distance(listPartCorrect[i].transform.position, listPartCorrect[j].transform.position);
                float angle_2 = Vector3.Angle(listPartCorrect[i].transform.position, listPartCorrect[j].transform.position);


                float checkDis = Mathf.Abs(listPart[i].transform.position.x - listPart[j].transform.position.x);
                if (checkDis >= 0 && checkDis < 0.01f)
                {
                    pointAccuracy += 1;
                }
                else if (checkDis >= 0.01f && checkDis < 0.04f)
                {
                    pointAccuracy += 0.5f;
                }
                else if (checkDis >= 0.04f && checkDis < 0.07f)
                {
                    pointAccuracy += 0.1f;
                }
                else if (checkDis >= 0.07f)
                {
                    pointAccuracy = 0;
                    waitCheckCorrect = false;
                    return false;
                }

            }

            float checkHeight = Mathf.Abs(listPart[i].transform.position.y - listPartCorrect[i].transform.position.y);
            if (checkHeight >= 0 && checkHeight < 0.01f)
            {
                pointAccuracy += 1;
            }
            else if (checkHeight >= 0.01f && checkHeight < 0.02f)
            {
                pointAccuracy += 0.5f;
            }
            else if (checkHeight >= 0.02f && checkHeight < 0.06f)
            {
                pointAccuracy += 0.1f;
            }
            else if (checkHeight >= 0.06f)
            {
                pointAccuracy = 0;
                waitCheckCorrect = false;
                return false;
            }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour
{
    public List<Transform> listLinePoint;


    public void SetPoint(Vector3 _point1, Vector3 _point2, Vector3 _point3)
    {

        listLinePoint[0].position = _point1;
        listLinePoint[1].position = _point2;
        listLinePoint[2].position = _point3;
    }
}

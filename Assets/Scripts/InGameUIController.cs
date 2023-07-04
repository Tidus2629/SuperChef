using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{

    public GameObject rateGroup;
    public GameObject[] stars;

    public void ShowRate(int star)
    {
        rateGroup.SetActive(true);
        StartCoroutine(CoShowRate(star));
    }

    private IEnumerator CoShowRate(int star)
    {
        for (int i = 0; i < star; i++)
        {
            stars[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}

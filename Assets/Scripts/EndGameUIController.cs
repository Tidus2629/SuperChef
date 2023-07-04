using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject winGroup;
    [SerializeField]
    private GameObject loseGroup;
    void Start()
    {
        winGroup.SetActive(GameManager.Instance.isWin);
        loseGroup.SetActive(!GameManager.Instance.isWin);
    }

  
}

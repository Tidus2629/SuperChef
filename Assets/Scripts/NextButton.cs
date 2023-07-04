using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Level_1");
    }
}

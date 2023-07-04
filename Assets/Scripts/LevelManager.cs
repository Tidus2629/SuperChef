using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Player player;

    public List<ObjectInteract> listObject;
    private bool isActive;
    public Animator doorAnimator;
    public Transform guest;
    public Transform pointService;
    public GameObject uiItem;
    public Transform pointCamPlay;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Invoke("ActiveObject", 2);
    }

    public void ClickPlay()
    {
        Intro();
    }


    public void Intro()
    {
        guest.gameObject.SetActive(true);
        doorAnimator.enabled = true;
        guest.DOMove(pointService.position, 5).OnComplete(() =>
        {
            ShowUI();
        });
    }

    public void ShowUI()
    {
        guest.GetComponent<Animator>().SetTrigger("Talk");
        uiItem.SetActive(true);
        Invoke("StartPlay", 3);
    }

    public void StartPlay()
    {
        uiItem.SetActive(false);
        player.transform.DOMove(pointCamPlay.position, 1f);
        player.transform.DORotate(pointCamPlay.eulerAngles, 1f);
        player.enabled = true;
        player.hand.gameObject.SetActive(true);
        ActiveObject();
    }

    public void ActiveObject()
    {
        if (isActive)
            return;
        isActive = true;
        for (int i = 0; i < listObject.Count; i++)
        {
            listObject[i].gameObject.SetActive(true);
            listObject[i].Active();
        }
    }

    private void OnDestroy()
    {
        Instance = null;
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

   
}

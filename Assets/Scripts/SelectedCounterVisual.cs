using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs args)
    {
        if(args.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        foreach(GameObject gameObject in visualGameObjectArray)
        {
            gameObject.SetActive(false);
        }
    }

    private void Show()
    {
        foreach (GameObject gameObject in visualGameObjectArray)
        {
            gameObject.SetActive(true);
        }
    }
}

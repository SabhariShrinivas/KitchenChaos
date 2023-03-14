using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    private float timer;
    private int timerInt;
    private string examplestring;
    [SerializeField] float loadTimerMax = 3f;
    void Update()
    {
        timer += Time.deltaTime;
        timerInt = Mathf.RoundToInt(timer);
        loadingText.text = "LOADING";
        for (int i = 0; i < timerInt; i++)
        {
            loadingText.text += ".";
        }
        if(timer > loadTimerMax)
        {
            timer = 0;
        }
        
    }
}

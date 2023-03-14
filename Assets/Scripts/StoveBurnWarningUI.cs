using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        Hide();
    }

    private void StoveCounter_OnStateChanged(StoveCounter.State obj)
    {
        if(obj != StoveCounter.State.Fried)
        {
            Hide();
        }
    }

    private void StoveCounter_OnProgressChanged(float obj)
    {
        float burnShowProgressAmount = 0f;
        bool show = stoveCounter.IsFried() && obj > burnShowProgressAmount;
        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private float loadTimer;
    private float loadTimerMax = 3f;

    private void Update()
    {
        loadTimer += Time.deltaTime;
        if(loadTimer > loadTimerMax)
        {
            loadTimer = 0;
            Loader.LoaderCallback();
        }          
    }
}

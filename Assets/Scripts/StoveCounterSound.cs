using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource;
    private bool playWarningSound;
    private float warningSoundTimer;
    private float warningSoundTimerMax = .2f;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(float obj)
    {
        float burnShowProgressAmount = 0f;
        playWarningSound = stoveCounter.IsFried() && obj > burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(StoveCounter.State obj)
    {
        bool playSound = obj == StoveCounter.State.Frying || obj == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
        if(obj != StoveCounter.State.Fried)
        {
            playWarningSound = false;
        }
    }
    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if(warningSoundTimer < 0)
            {
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(transform.position);
            }
        }
    }
}

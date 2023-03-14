using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    Animator animator;
    private const string STOVE_FLASHING = "IsFlashing";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(STOVE_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(float obj)
    {
        float burnShowProgressAmount = 0f;
        bool show = stoveCounter.IsFried() && obj > burnShowProgressAmount;
        animator.SetBool(STOVE_FLASHING, show);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject IHasProgressGameObject;
    [SerializeField] Image barImage;
    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = IHasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError($"GameObject {this} does not have a component that implements  IHasProgress");
        }
        hasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;
        hasProgress.OnKitchenObjectBeingCooked += IHasProgress_OnKitchenBeingCooked;
        gameObject.SetActive(false);
    }

    private void IHasProgress_OnKitchenBeingCooked(bool obj)
    {
        if(obj == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void IHasProgress_OnProgressChanged(float obj)
    {
        //barImage.DOFillAmount(obj, 0.2f).SetEase(Ease.InOutQuint);
        barImage.fillAmount = obj;
    }
}

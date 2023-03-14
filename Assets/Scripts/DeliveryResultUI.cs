using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage, icon;
    [SerializeField] private TextMeshProUGUI deliveryText;
    [SerializeField] private Color successColor, failColor;
    [SerializeField] Sprite successSprite, failSprite;
    [SerializeField] CanvasGroup canvasGroup;
    private Animator animator;
    private const string POPUP = "Popup";
    private void Start()
    {
        DeliveryManager.Instance.OnRecipieFailed += DeliveryManager_OnRecipieFailed;
        DeliveryManager.Instance.OnRecipieSuccess += DeliveryManager_OnRecipieSuccess;
        animator = GetComponent<Animator>();
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipieSuccess()
    {
        canvasGroup.alpha = 1;
        backgroundImage.color = Color.green;
        icon.sprite = successSprite;
        deliveryText.text = "DELIVERY\nSUCCESS";
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
    }

    private void DeliveryManager_OnRecipieFailed()
    {
        canvasGroup.alpha = 1;
        backgroundImage.color = Color.red;
        icon.sprite = failSprite;
        deliveryText.text = "DELIVERY\nFAILED";
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
    }
}

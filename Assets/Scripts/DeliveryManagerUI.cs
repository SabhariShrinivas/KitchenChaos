using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipieTemplate;

    private void Awake()
    {
        recipieTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipieSpawned += DeliveryManager_OnRecipieSpawned;
        DeliveryManager.Instance.OnRecipieDelivered += DeliveryManager_OnRecipieDelivered;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipieDelivered()
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipieSpawned()
    {
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if(child == recipieTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach(RecipieSO recipieSO in DeliveryManager.Instance.GetWaitingRecipieSOList())
        {
            Transform recipie =  Instantiate(recipieTemplate, container);
            recipie.gameObject.SetActive(true);
            recipie.GetComponent<DeliveryManagerSingleUI>().SetRecipieSO(recipieSO);
        }
    }
}

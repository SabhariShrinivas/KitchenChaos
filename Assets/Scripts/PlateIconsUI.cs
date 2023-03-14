using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject PlateKitchenObject;
    [SerializeField] private PlateIconSingleUI iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        PlateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(KitchenObjectSO obj)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if(child == iconTemplate.transform)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in PlateKitchenObject.GetKitchenObjectSOList())
        {
            PlateIconSingleUI plateIconSingleUI = Instantiate(iconTemplate, transform);
            plateIconSingleUI.gameObject.SetActive(true);
            plateIconSingleUI.SetKitchenObjectImage(kitchenObjectSO);
        }
    } 
}

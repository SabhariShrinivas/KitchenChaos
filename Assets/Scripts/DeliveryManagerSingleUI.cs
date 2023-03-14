using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipieNameText;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipieSO(RecipieSO recipieSO)
    {
        recipieNameText.text = recipieSO.recipieName;

        foreach(Transform child in iconContainer)
        {
            if(child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObject in recipieSO.kitchenObjectSOList)
        {
            Transform icon = Instantiate(iconTemplate, iconContainer);
            icon.GetComponent<Image>().sprite = kitchenObject.iconSprite;
            icon.gameObject.SetActive(true);
        }
    }
}

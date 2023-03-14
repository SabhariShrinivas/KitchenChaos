using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            //only accept plate
            DeliveryManager.Instance.DeliverRecipie(plateKitchenObject);
            player.GetKitchenObject().SetKitchenObjectParentAndDestroySelf(this);
        }
    }
}

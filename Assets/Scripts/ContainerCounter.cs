using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO KitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;
    public override void Interact(Player player)
    {
        if (!player.HasAnyKitchenObject())
        {
            KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(KitchenObjectSO, this);
            kitchenObject.transform.localPosition = Vector3.zero;
            kitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }
}

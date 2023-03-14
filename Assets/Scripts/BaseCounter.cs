using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] Transform spawnTransform;
    public static event EventHandler OnAnyObjectPlaced;
    public static void ResetStaticData()
    {
        OnAnyObjectPlaced = null;
    }
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {

    }
    public virtual void InteractAlternate(Player player)
    {

    }
    public Transform GetSpawnTransform()
    {
        return spawnTransform;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(this.kitchenObject != null)
        {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasAnyKitchenObject()
    {
        return kitchenObject != null;
    }
}

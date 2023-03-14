using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return KitchenObjectSO;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
           this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasAnyKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a kitchen Object");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetSpawnTransform();
        transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutQuint);
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public void SetKitchenObjectParentAndDestroySelf(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasAnyKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a kitchen Object");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetSpawnTransform();
        transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutQuint).OnComplete(() =>
        {
            kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        });
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, Vector3.zero, Quaternion.identity, kitchenObjectParent.GetSpawnTransform());
        kitchenObjectTransform.localPosition = Vector3.zero;
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObjectTransform.GetComponent<KitchenObject>();
    }
}

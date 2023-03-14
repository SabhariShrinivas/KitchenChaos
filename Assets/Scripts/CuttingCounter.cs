using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipieSO[] cuttingRecipieArray;
    private int cuttingProgress;
    public event Action<float> OnProgressChanged;
    public event Action<bool> OnKitchenObjectBeingCooked;
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public event Action OnCut;
    public override void Interact(Player player)
    {
        if (!HasAnyKitchenObject())
        {
            //container doesnt have any kitchen obj
            if (player.HasAnyKitchenObject())
            {
              //  player.GetKitchenObject().SetKitchenObjectParent(this);             
                if (HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Only able to drop if the kitchen Object is cuttable
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(cuttingProgress);
                    OnKitchenObjectBeingCooked?.Invoke(true);
                }

            }
            else
            {
                //player doesnt have any kitchen Object
            }
        }
        else
        {
            //container already has a kitchen object
            if (player.HasAnyKitchenObject())
            {
                //player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is carrying a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                OnKitchenObjectBeingCooked?.Invoke(false);
            }
        }

    }
    public override void InteractAlternate(Player player)
    {
        if (HasAnyKitchenObject() && HasRecipieWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke();
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke((float)cuttingProgress / cuttingRecipieSO.cuttingProgressMax);
            if(cuttingProgress >= cuttingRecipieSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                OnKitchenObjectBeingCooked?.Invoke(false);
            }
            
        }
    }

    private bool HasRecipieWithInput(KitchenObjectSO input)
    {
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(input);
        return cuttingRecipieSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(input);
        if(cuttingRecipieSO != null)
        {
            return cuttingRecipieSO.output;
        }
        return null;
    }

    private CuttingRecipieSO GetCuttingRecipieSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipieSO cuttingRecipie in cuttingRecipieArray)
        {
            if (cuttingRecipie.input == kitchenObjectSO)
            {
                return cuttingRecipie;
            }
        }
        return null;
    }
}

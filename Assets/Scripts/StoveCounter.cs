using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    public event Action<State> OnStateChanged;
    public event Action<float> OnProgressChanged;
    public event Action<bool> OnKitchenObjectBeingCooked;

    [SerializeField] private FryingRecipieSO[] fryingRecipieSOArray;
    [SerializeField] private BurningRecipieSO[] burningRecipieSOArray;
    private FryingRecipieSO fryingRecipieSO;
    private BurningRecipieSO burningRecipieSO;
    private State currentState;
    // Start is called before the first frame update
    private float fryingTimer;
    private float burningTimer;

    private void Start()
    {
        currentState = State.Idle;
    }
    private void Update()
    {
        if (HasAnyKitchenObject())
        {
            switch (currentState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(fryingTimer / fryingRecipieSO.fryingTimerMax);
                    if (fryingTimer >= fryingRecipieSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipieSO.output, this);
                        currentState = State.Fried;
                        burningTimer = 0;
                        burningRecipieSO = GetBurningRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(currentState);
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(burningTimer / burningRecipieSO.burningTimerMax);
                    if (burningTimer >= burningRecipieSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipieSO.output, this);
                        currentState = State.Burned;
                        OnStateChanged?.Invoke(currentState);
                    }
                    break;
                case State.Burned:
                    OnKitchenObjectBeingCooked?.Invoke(false);
                    break;
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!HasAnyKitchenObject())
        {
            //container doesnt have any kitchen obj
            if (player.HasAnyKitchenObject())
            {
                //player has a kitchenObject
                if (HasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player is carring something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    OnKitchenObjectBeingCooked?.Invoke(true);
                    fryingRecipieSO = GetFryingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    currentState = State.Frying;
                    OnStateChanged?.Invoke(currentState);
                    fryingTimer = 0f;
                    OnProgressChanged?.Invoke(0);
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
                        currentState = State.Idle;
                        OnStateChanged?.Invoke(currentState);
                        OnKitchenObjectBeingCooked?.Invoke(false);
                    }
                }
            }
            else
            {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                currentState = State.Idle;
                OnStateChanged?.Invoke(currentState);
                OnKitchenObjectBeingCooked?.Invoke(false);

            }
        }

    }

    private bool HasRecipieWithInput(KitchenObjectSO input)
    {
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(input);
        return fryingRecipieSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(input);
        if (fryingRecipieSO != null)
        {
            return fryingRecipieSO.output;
        }
        return null;
    }

    private FryingRecipieSO GetFryingRecipieSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipieSO fryingRecipie in fryingRecipieSOArray)
        {
            if (fryingRecipie.input == kitchenObjectSO)
            {
                return fryingRecipie;
            }
        }
        return null;
    }
    private BurningRecipieSO GetBurningRecipieSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurningRecipieSO burningRecipieSO in burningRecipieSOArray)
        {
            if (burningRecipieSO.input == kitchenObjectSO)
            {
                return burningRecipieSO;
            }
        }
        return null;
    }
    public bool IsFried()
    {
        return currentState == State.Fried;
    }
}

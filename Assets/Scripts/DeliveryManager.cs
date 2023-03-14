using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipieListSO RecipieListSO;
    public event Action OnRecipieSpawned;
    public event Action OnRecipieDelivered;
    public event Action OnRecipieSuccess;
    public event Action OnRecipieFailed;
    private List<RecipieSO> waitingRecipeSOList = new List<RecipieSO>();
    private int waitingRecipieMax = 7;
    private float spawnRecipieTimer = 5;
    private float spawnRecipieTimerMax = 20;
    private int successfulRecipieDelivered;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Instance found");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        spawnRecipieTimer -= Time.deltaTime;
        if (spawnRecipieTimer < 0)
        {
            if (GameStateManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipieMax)
            {
                spawnRecipieTimer = spawnRecipieTimerMax;
                RecipieSO waitingRecipieSO = RecipieListSO.recipieSOList[UnityEngine.Random.Range(0, RecipieListSO.recipieSOList.Count)];
                Debug.Log(waitingRecipieSO.recipieName);
                waitingRecipeSOList.Add(waitingRecipieSO);
                OnRecipieSpawned?.Invoke();
            }
        }
    }
    public void DeliverRecipie(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipieSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipieKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cycling through all ingridients in the recipie
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycling through all ingridients in the plate
                        if (plateKitchenObjectSO == recipieKitchenObjectSO)
                        {
                            //IngredientMatches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //this recipie ingridient was not found in the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    //player delivered the correct recipie
                    successfulRecipieDelivered++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipieDelivered?.Invoke();
                    OnRecipieSuccess?.Invoke();
                    return;
                }
            }
        }
        //no matches found
        OnRecipieFailed?.Invoke();
    }

    public List<RecipieSO> GetWaitingRecipieSOList()
    {
        return waitingRecipeSOList;
    }
    public int GetSuccessfulReciepieAmount()
    {
        return successfulRecipieDelivered;
    }
}

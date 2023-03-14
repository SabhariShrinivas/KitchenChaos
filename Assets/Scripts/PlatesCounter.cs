using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObject;
    [SerializeField] private PlatesCounterVisual PlatesCounterVisual;
    private float plateSpawnTimer;
    private float plateSpawnTimerMax = 4f;
    private int plateSpawnAmount;
    private int plateSPawnAmountMax = 4;
    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;
    // Update is called once per frame
    void Update()
    {
        plateSpawnTimer += Time.deltaTime;
        if(GameStateManager.Instance.IsGamePlaying() && plateSpawnTimer > plateSpawnTimerMax)
        {
            plateSpawnTimer = 0f;
            if(plateSpawnAmount < plateSPawnAmountMax)
            {
                plateSpawnAmount++;
                OnPlateSpawned?.Invoke();
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasAnyKitchenObject())
        {
            if(plateSpawnAmount > 0)
            {
                plateSpawnAmount--;
                //KitchenObject.SpawnKitchenObject(plateKitchenObject, this);
                //GetKitchenObject().SetKitchenObjectParent(player);
                Transform kitchenObjectTransform = Instantiate(plateKitchenObject.prefab, GetSpawnTransform());
                kitchenObjectTransform.localPosition = PlatesCounterVisual.ReturnLastPlateLocalPosition();
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                OnPlateRemoved?.Invoke();
            }
        }
    }
}

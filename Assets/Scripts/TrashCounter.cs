using System;
using System.Collections;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    private bool isPlaying = false;
    public static event EventHandler OnAnyObjectTrashed;
    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasAnyKitchenObject() && !isPlaying)
        {
            isPlaying = true;
            player.GetKitchenObject().SetKitchenObjectParent(this);
            StartCoroutine(Destroy());
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(.5f);
        GetKitchenObject().DestroySelf();
        isPlaying = false;
    }
}

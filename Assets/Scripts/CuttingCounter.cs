using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject)
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject)
            {
                if(HasCuttingRecipeWithInputKitchenObject(player.GetKitchenObject().KitchenObjectSO))
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            // There is a KitchenObject here
            if (player.HasKitchenObject)
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }



    public override void InteractAlternate()
    {
        if (HasKitchenObject)
        {
            if (HasCuttingRecipeWithInputKitchenObject(GetKitchenObject().KitchenObjectSO))
            {
                KitchenObjectSO outputKitchenObjectSO = GetKitchenObjectOutput(GetKitchenObject().KitchenObjectSO);
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
           
        }
    }

    private KitchenObjectSO GetKitchenObjectOutput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipesSO)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO) return cuttingRecipeSO.output;
        }

        return null;
    }

    private bool HasCuttingRecipeWithInputKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipesSO)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO) return true;
        }

        return false;
    }
}

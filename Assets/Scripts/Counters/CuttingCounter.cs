using System;
using UnityEngine;
using static IHasProgress;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipesSO;

    private int cuttingProgress = 0;

    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;
   

    public EventHandler OnCut;
    public static EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject)
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject)
            {
                if (HasCuttingRecipeWithInputKitchenObject(player.GetKitchenObject().KitchenObjectSO))
                {
                    // Player is carrying something
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    ResetCuttingProgress();
                }

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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                ResetCuttingProgress();
            }
        }
    }



    public override void InteractAlternate()
    {
        if (HasKitchenObject)
        {
            if (HasCuttingRecipeWithInputKitchenObject(GetKitchenObject().KitchenObjectSO))
            {
                CuttingRecipeSO cuttingRecipeSO  = GetCuttingRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);
                cuttingProgress++;

                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);

                OnProgressChanged?.Invoke(this,new OnProgressChangedEventsArgs { 
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax 
                });

                if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    KitchenObjectSO outputKitchenObjectSO = GetKitchenObjectOutput(GetKitchenObject().KitchenObjectSO);
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                    ResetCuttingProgress();
                }
            }
           
        }
    }

    private KitchenObjectSO GetKitchenObjectOutput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        if (cuttingRecipeSO != null) return cuttingRecipeSO.output;
        return null;
    }

    private bool HasCuttingRecipeWithInputKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipesSO)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO) return cuttingRecipeSO;
        }
        
        return null;
    }

    private void ResetCuttingProgress()
    {
        cuttingProgress = 0;
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs
        {
            progressNormalized = cuttingProgress
        });
    }
}

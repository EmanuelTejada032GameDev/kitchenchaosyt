using UnityEngine;

public class StoveCounter : BaseCounter
{

    [SerializeField] private FryingRecipeSO[] fryingRecipesSO;


    private float fryingTimer;
    private FryingRecipeSO currentFryingRecipe;

    private void Update()
    {
        if (HasKitchenObject)
        {
            fryingTimer += Time.deltaTime;
            if(fryingTimer > currentFryingRecipe.fryingTimerMax)
            {
                fryingTimer = 0;
                Debug.Log("Fried");
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(currentFryingRecipe.output,this);
            }
            Debug.Log(fryingTimer);
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject)
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject)
            {
                if (HasFryingRecipeWithInputKitchenObject(player.GetKitchenObject().KitchenObjectSO))
                {
                    // Player is carrying something
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    currentFryingRecipe = GetFryingRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);
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
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    private KitchenObjectSO GetKitchenObjectOutput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        if (fryingRecipeSO != null) return fryingRecipeSO.output;
        return null;
    }

    private bool HasFryingRecipeWithInputKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var fryingRecipeSO in fryingRecipesSO)
        {
            if (fryingRecipeSO.input == kitchenObjectSO) return fryingRecipeSO;
        }

        return null;
    }

    private void ResetFryingTimer()
    {
        fryingTimer = 0;
    }

}

using UnityEngine;

public class StoveCounter : BaseCounter
{

    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipesSO;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private FryingRecipeSO currentFryingRecipe;

    private float burningTimer;
    private BurningRecipeSO currentBurningRecipe;

    private State state;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject)
        {
            switch (state)
            {
                case State.Idle: 
                    break; 
                case State.Frying:
                        fryingTimer += Time.deltaTime;
                        if (fryingTimer > currentFryingRecipe.fryingTimerMax)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(currentFryingRecipe.output, this);
                            state = State.Fried;
                            ResetBurningTimer();
                            currentBurningRecipe = GetBurningRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);
                        }
                    break; 
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer > currentBurningRecipe.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(currentBurningRecipe.output, this);
                        state = State.Burned;
                    }
                    break; 
                case State.Burned: 
                    break;
            }
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
                    state = State.Frying;
                    ResetFryingTimer();
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
                ResetFryingTimer();
                ResetBurningTimer();
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
    
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == kitchenObjectSO) return burningRecipeSO;
        }

        return null;
    }

    private void ResetFryingTimer()
    {
        fryingTimer = 0;
    }   
    
    private void ResetBurningTimer()
    {
        burningTimer = 0;
    }

}

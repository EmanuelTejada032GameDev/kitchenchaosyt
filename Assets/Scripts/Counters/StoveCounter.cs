using System;
using UnityEngine;
using static IHasProgress;

public class StoveCounter : BaseCounter, IHasProgress
{

    public EventHandler<OnStoveStateChangeEventArgs> OnStateChange;
    public class OnStoveStateChangeEventArgs
    {
        public State state;
    }

    public enum State
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

    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;

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
                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs { progressNormalized = (float)fryingTimer / currentFryingRecipe.fryingTimerMax });
                        if (fryingTimer > currentFryingRecipe.fryingTimerMax)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(currentFryingRecipe.output, this);
                            SetState(State.Fried);
                            ResetBurningTimer();
                            currentBurningRecipe = GetBurningRecipeSOWithInput(GetKitchenObject().KitchenObjectSO);
                        }
                    break; 
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs { progressNormalized = (float)burningTimer / currentBurningRecipe.burningTimerMax });

                    if (burningTimer > currentBurningRecipe.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(currentBurningRecipe.output, this);
                        SetState(State.Burned);
                        ResetBurningTimer();
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
                    SetState(State.Frying);
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSO))
                    {
                        GetKitchenObject().DestroySelf();
                        SetState(State.Idle);
                        ResetFryingTimer();
                        ResetBurningTimer();
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                SetState(State.Idle);
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
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs { progressNormalized = fryingTimer });

    }

    private void ResetBurningTimer()
    {
        burningTimer = 0;
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventsArgs { progressNormalized = burningTimer });
    }

    public void SetState(State newState)
    {
        state = newState;
        OnStateChange?.Invoke(this, new OnStoveStateChangeEventArgs { state = newState});
    }

}

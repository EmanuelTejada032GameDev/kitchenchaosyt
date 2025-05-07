using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs {
        public KitchenObjectSO KitchenObjectSO;
    }

    private List<KitchenObjectSO> kitchenObjectsSOList;
    public List<KitchenObjectSO> validPlateKitchenObjects;

    private void Awake()
    {
        kitchenObjectsSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validPlateKitchenObjects.Contains(kitchenObjectSO)) return false;
        if (kitchenObjectsSOList.Contains(kitchenObjectSO)) return false;
        kitchenObjectsSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this,new OnIngredientAddedEventArgs { KitchenObjectSO = kitchenObjectSO});
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectsSOList;
    }

}

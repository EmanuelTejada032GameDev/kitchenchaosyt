using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    private List<KitchenObjectSO> kitchenObjectsSOs;
    public List<KitchenObjectSO> validPlateKitchenObjects;

    private void Awake()
    {
        kitchenObjectsSOs = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validPlateKitchenObjects.Contains(kitchenObjectSO)) return false;
        if (kitchenObjectsSOs.Contains(kitchenObjectSO)) return false;
        kitchenObjectsSOs.Add(kitchenObjectSO);
        return true;
    }

}

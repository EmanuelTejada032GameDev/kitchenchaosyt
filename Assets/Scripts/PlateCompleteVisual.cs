using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    private struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectsList;

    [SerializeField] private PlateKitchenObject plateKitchenObjectParent;

    private void Start()
    {
        plateKitchenObjectParent.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject item in kitchenObjectSO_GameObjectsList)
        {
            if(e.KitchenObjectSO == item.kitchenObjectSO)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}

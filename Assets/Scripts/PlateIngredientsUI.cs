using System;
using UnityEngine;

public class PlateIngredientsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plate.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plate.GetKitchenObjectSOList())
        {
            Transform iconTemplateTransform = Instantiate(iconTemplate,transform);
            iconTemplateTransform.gameObject.GetComponent<IconTemplate>().Setup(kitchenObjectSO);
            iconTemplateTransform.gameObject.SetActive(true);
        }
    }
}


using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CuttingRecipe", fileName = "CuttingRecipe")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingProgressMax;
}

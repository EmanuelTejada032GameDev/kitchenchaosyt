
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FryingRecipe", fileName = "FryingRecipe")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;
}


using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BurningRecipe", fileName = "BurningRecipe")]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;
}

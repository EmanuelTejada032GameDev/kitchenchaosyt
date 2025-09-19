
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecipeSO", fileName = "RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> ingredients;
    public string recipeName;
}

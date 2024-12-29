using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/KitchenObjectsSO/Ingredients" , fileName = "IngredientSO")]
public class KitchenObjectSO : ScriptableObject
{
    public string objectName;
    public GameObject prefab;
    public Sprite icon;
}

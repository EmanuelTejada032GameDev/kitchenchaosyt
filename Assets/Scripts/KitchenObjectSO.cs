using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "KitchenObjectsSO/Ingredients")]
public class KitchenObjectSO : ScriptableObject
{
    public string objectName;
    public GameObject prefab;
    public Sprite icon;
}

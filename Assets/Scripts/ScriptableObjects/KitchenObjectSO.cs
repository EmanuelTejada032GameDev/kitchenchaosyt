using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/KitchenObject" , fileName = "KitchenObject")]
public class KitchenObjectSO : ScriptableObject
{
    public string objectName;
    public GameObject prefab;
    public Sprite icon;
}

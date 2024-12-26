using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTop;

    public void Interact()
    {
        Debug.Log($"Will spawn a {kitchenObjectSO.objectName}");
        GameObject spawnedPrefab = Instantiate(kitchenObjectSO.prefab, counterTop);
        Debug.Log($"Created object {spawnedPrefab.name}");

        spawnedPrefab.transform.localPosition = Vector3.zero;   
    }
}

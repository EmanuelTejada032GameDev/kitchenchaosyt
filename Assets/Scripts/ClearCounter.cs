using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTop;

    private KitchenObject kitchenObject;
    [SerializeField] private ClearCounter secondClearCounter;

    [SerializeField] bool testing = false;

    private void Update()
    {
        if(testing && Input.GetKey(KeyCode.T))
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }


    public void Interact()
    {
        if (kitchenObject == null)
        {
            GameObject spawnedPrefab = Instantiate(kitchenObjectSO.prefab, counterTop);
            spawnedPrefab.GetComponent<KitchenObject>().SetClearCounter(this);   
        }else
        {
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTop;
    }


    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject => kitchenObject != null;  
}

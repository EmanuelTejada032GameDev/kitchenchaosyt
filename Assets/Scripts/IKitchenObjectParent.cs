using UnityEngine;

public interface IKitchenObjectParent 
{
    bool HasKitchenObject { get; }
    void ClearKitchenObject();
    KitchenObject GetKitchenObject();
    Transform GetKitchenObjectFollowTransform();
    void SetKitchenObject(KitchenObject kitchenObject);
}

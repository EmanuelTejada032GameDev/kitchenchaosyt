using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTop;
    private KitchenObject kitchenObject;


    public virtual void Interact(Player player) {
        Debug.LogError("This should never be called");
    }

    public virtual void InteractAlternate() {
        Debug.LogError("This should never be called");
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

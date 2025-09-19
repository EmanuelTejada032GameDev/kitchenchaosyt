using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTop;
    private KitchenObject kitchenObject;
    public static EventHandler OnAnyItemDroppedHere;

    public virtual void Interact(Player player) {
        Debug.LogError("This should never be called, Base Interact");
    }

    public virtual void InteractAlternate() {
        //Debug.LogError("This should never be called");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTop;
    }


    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null )
        {
            OnAnyItemDroppedHere?.Invoke(this, EventArgs.Empty);
        }
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

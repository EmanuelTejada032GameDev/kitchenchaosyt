using System;
using System.Threading.Tasks;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public EventHandler OnContainerCounterInteract;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject)
        {
            OnContainerCounterInteract?.Invoke(this, EventArgs.Empty);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }

    }
}

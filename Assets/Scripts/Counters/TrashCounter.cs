using System;

public class TrashCounter : BaseCounter
{

    public static EventHandler OnAnyItemTrashed;

    public override void Interact(Player player)
    {
            if (player.HasKitchenObject)
            {
                player.GetKitchenObject().DestroySelf();
            OnAnyItemTrashed?.Invoke(this, EventArgs.Empty);
            }
    }

}

using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        //trash counter
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            //counter doesn't have a kitchen object
            if (player.HasKitchenObject())
            {
                //player has a kitchen object
                player.GetKitchenObjects().SetKitchenObjectParent(this);
            }
            else
            {
             // player doesn't have a kitchen object 
            }

        }
        else
        {
            //counter has a kitchen object
            if (player.HasKitchenObject())
            {
                //KitchenObjects playerKitchenObject = player.GetKitchenObjects();
                //KitchenObjects counterKitchenObject= GetKitchenObjects();
                //player has a kitchen object
                //player.ClearKitchenObjects();
                //ClearKitchenObjects();

                //// Swap parents
                //playerKitchenObject.SetKitchenObjectParent(this);
                //counterKitchenObject.SetKitchenObjectParent(player);
            }
            else
            {
                //player doesn't have a kitchen object
                GetKitchenObjects().SetKitchenObjectParent(player);
            }

        }
        
    }

}
